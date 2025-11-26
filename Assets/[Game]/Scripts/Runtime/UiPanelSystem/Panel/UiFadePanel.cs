using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class UiFadePanel : UiPanel
{
    protected virtual float FadeInDuration => 0.5f;
    protected virtual float FadeOutDuration => 0.5f;

    protected virtual float ShowDelay => 0;
    protected virtual float HideDelay => 0;

    protected virtual float MaxFade => 1f;
    protected virtual float MinFade => 0f;

    private Tween _tween;

    protected virtual void OnDisable()
    {
        _tween?.Kill();
        _tween = null;
    }

    [ButtonGroup("PanelVisibility")]
    public virtual void ShowPanelAnimated()
    {
        if (IsVisible)
            return;

        FadeTween(MaxFade, ShowDelay, FadeInDuration, Show);
    }

    [ButtonGroup("PanelVisibility")]
    public virtual void HidePanelAnimated()
    {
        if (!IsVisible)
            return;

        FadeTween(MinFade, HideDelay, FadeOutDuration, Hide);
    }

    protected virtual void FadeTween(float endValue, float delay, float duration, Action onComplete = null)
    {
        DOTween.Kill(_tween);
        _tween = CanvasGroup.DOFade(endValue, duration).SetUpdate(true).SetDelay(delay).SetEase(Ease.Linear).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}
