using UnityEngine;
using Sirenix.OdinInspector;

public class SpinSlotItem : BaseMultiEventListener
{
    [SerializeField] private BoxCollider2D _collider;

    private void OnValidate()
    {
        if (_collider == null)
            _collider = GetComponentInChildren<BoxCollider2D>();
    }

    private void OnEnable()
    {
        AddHandler<SpinStartedEvent>(OnSpinStarted);
        AddHandler<SpinCompletedEvent>(OnSpinCompleted);
    }

    private void OnSpinStarted(SpinStartedEvent startedEvent)
    {
        _collider.enabled = true;
    }

    private void OnSpinCompleted(SpinCompletedEvent completedEvent)
    {
        _collider.enabled = false;
    }
}
