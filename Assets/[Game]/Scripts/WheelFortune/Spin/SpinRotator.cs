using UnityEngine;
using DG.Tweening;

public class SpinRotator : MonoBehaviour, ISpinRotator
{
    [SerializeField] private Transform wheelRoot;
    public bool IsSpinning { get; private set; }
    private Tween _tween;
    private float _finalAngle;

    private void OnDisable()
    {
        _tween?.Kill();
        IsSpinning = false;
    }

    public void RotateTo(float targetAngle, float duration)
    {
        if (IsSpinning)
            return;

        IsSpinning = true;
        _finalAngle = targetAngle;

        _tween?.Kill();
        _tween = wheelRoot
            .DOLocalRotate(
                new Vector3(0f, 0f, targetAngle),
                duration,
                RotateMode.FastBeyond360
            )
            .SetEase(Ease.OutQuart)
            .OnComplete(OnSpinCompleted);
    }

    private void OnSpinCompleted()
    {
        IsSpinning = false;
        EventManager.Raise(new SpinCompletedEvent(_finalAngle));
    }
}
