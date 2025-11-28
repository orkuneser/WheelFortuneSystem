using UnityEngine;
using DG.Tweening;

public class UiPanelScaleAnimation : UiPanelAnimationBase
{
    private Tween _tween;

    private void OnDisable()
    {
        _tween?.Kill();
        _tween = null;
    }

    public override void DoShowAnimation()
    {
        _tween?.Kill();

        Panel.SetPanel(1f, true, true);

        transform.localScale = Vector3.zero;

        _tween = transform
            .DOScale(Vector3.one, Duration)
            .SetEase(ShowEase);
    }

    public override void DoHideAnimation()
    {
        _tween?.Kill();

        transform.localScale = Vector3.one;

        _tween = transform
            .DOScale(Vector3.zero, Duration)
            .SetEase(HideEase)
            .OnComplete(() =>
            {
                Panel.SetPanel(0f, false, false);
            });
    }
}
