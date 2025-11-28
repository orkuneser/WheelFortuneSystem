using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class SpinIndicator : MonoBehaviour
{
    [Title("Settings")]
    [SerializeField] private float targetAngle = -20f;
    [SerializeField] private float duration = 0.1f;
    [SerializeField] private float returnDuration = 0.12f;

    private Tween _tween;
    private Quaternion _initialRot;
    private bool _isAnimating;

    private void Awake()
    {
        _initialRot = transform.localRotation;
    }

    private void OnDisable()
    {
        _tween?.Kill();
        _isAnimating = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryPlayTick();
    }

    private void TryPlayTick()
    {
        if (_isAnimating)
            return;

        PlayTick();
    }

    [Button]
    private void PlayTick()
    {
        _isAnimating = true;

        _tween?.Kill();
        _tween = transform
            .DOLocalRotate(
                new Vector3(0, 0, targetAngle),
                duration
            )
            .SetEase(Ease.OutQuad)
            .OnComplete(ReturnBack);
    }

    private void ReturnBack()
    {
        _tween = transform
            .DOLocalRotate(
                _initialRot.eulerAngles,
                returnDuration
            )
            .SetEase(Ease.OutQuad)
            .OnComplete(() => _isAnimating = false);
    }
}