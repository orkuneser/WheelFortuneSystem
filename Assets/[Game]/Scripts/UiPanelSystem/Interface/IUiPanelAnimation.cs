using DG.Tweening;

public interface IUiPanelAnimation
{
    float Duration { get; set; }
    Ease ShowEase { get; set; }
    Ease HideEase { get; set; }

    void DoShowAnimation();
    void DoHideAnimation();
}
