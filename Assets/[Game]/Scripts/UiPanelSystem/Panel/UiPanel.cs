using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class UiPanel : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnShown = new UnityEvent();
    [HideInInspector] public UnityEvent OnHidden = new UnityEvent();

    private CanvasGroup _canvasGroup;
    protected CanvasGroup CanvasGroup => _canvasGroup ??= GetComponent<CanvasGroup>();

    private bool _isVisible;
    protected bool IsVisible => _isVisible;

    [ButtonGroup("PanelVisibility")]
    public virtual void Show()
    {
        if (IsVisible)
            return;

        _isVisible = true;
        IUiPanelAnimation animation = GetComponent<IUiPanelAnimation>();
        if (animation != null)
            animation.DoShowAnimation();
        else
            SetPanel(1f, true, true);

        OnShown?.Invoke();
    }

    [ButtonGroup("PanelVisibility")]
    public virtual void Hide()
    {
        if (!IsVisible)
            return;

        _isVisible = false;
        IUiPanelAnimation animation = GetComponent<IUiPanelAnimation>();
        if (animation != null)
            animation.DoHideAnimation();
        else
            SetPanel(0f, false, false);

        OnHidden?.Invoke();
    }

    [ButtonGroup("PanelVisibility")]
    public void Toggle()
    {
        if (IsVisible)
            Hide();
        else
            Show();
    }

    public void SetPanel(float alpha, bool interactable, bool blocksRaycast)
    {
        CanvasGroup.alpha = alpha;
        CanvasGroup.interactable = interactable;
        CanvasGroup.blocksRaycasts = blocksRaycast;
    }
}
