using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(UiPanel))]
public abstract class UiPanelAnimationBase : MonoBehaviour, IUiPanelAnimation
{
    private UiPanel _panel;
    protected UiPanel Panel => _panel ??= GetComponent<UiPanel>();

    [SerializeField] private float duration = 1f;
    public float Duration { get => duration; set => duration = value; }

    [SerializeField] private Ease showEase = Ease.OutBack;
    public Ease ShowEase { get => showEase; set => showEase = value; }

    [SerializeField] private Ease hideEase = Ease.InBack;
    public Ease HideEase { get => hideEase; set => hideEase = value; }

    public abstract void DoHideAnimation();
    public abstract void DoShowAnimation();
}
