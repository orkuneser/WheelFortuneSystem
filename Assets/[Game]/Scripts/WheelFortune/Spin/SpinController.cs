using UnityEngine;
using Sirenix.OdinInspector;

public class SpinController : BaseEventListener<SpinCompletedEvent>
{
    [Title("Spin Refs")]
    [SerializeField] private MonoBehaviour _rotatorSource;          // ISpinRotator
    [SerializeField] private MonoBehaviour _outcomeSelectorSource;  // IOutcomeSelector

    private ISpinRotator _rotator;
    private IOutcomeSelector _outcomeSelector;

    private void OnValidate()
    {
        AutoAssignInterface<ISpinRotator>(ref _rotatorSource);
        AutoAssignInterface<IOutcomeSelector>(ref _outcomeSelectorSource);
    }

    private void Awake()
    {
        CacheInterfaces();
    }

    private void CacheInterfaces()
    {
        if (_rotatorSource is ISpinRotator rotator)
            _rotator = rotator;

        if (_outcomeSelectorSource is IOutcomeSelector selector)
            _outcomeSelector = selector;
    }

    [Button]
    public void TryStartSpin()
    {
        if (_rotator == null || _outcomeSelector == null)
            CacheInterfaces();

        if (_rotator == null || _outcomeSelector == null)
            return;

        if (_rotator.IsSpinning)
            return;

        EventManager.Raise(new SpinStartedEvent());

        float angle = _outcomeSelector.GenerateTargetAngle();
        _rotator.RotateTo(angle, 2f);
    }

    protected override void OnEvent(SpinCompletedEvent completedEvent)
    {
        if (_outcomeSelector == null)
            CacheInterfaces();

        if (_outcomeSelector == null)
            return;

        _outcomeSelector.ResolveOutcome(completedEvent.FinalAngle);
    }

    private void AutoAssignInterface<TInterface>(ref MonoBehaviour source)
    {
        if (source != null && source is TInterface)
            return;

        var component = FindFirstObjectByType<MonoBehaviour>();
        if (component is TInterface)
            source = component;
    }
}
