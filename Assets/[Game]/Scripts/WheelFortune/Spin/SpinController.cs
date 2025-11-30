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
        if (_rotatorSource != null)
            _rotator = _rotatorSource as ISpinRotator;

        if (_outcomeSelectorSource != null)
            _outcomeSelector = _outcomeSelectorSource as IOutcomeSelector;

#if UNITY_EDITOR
        if (_rotator == null)
            Debug.LogError($"{nameof(SpinController)}: rotatorSource does not implement {nameof(ISpinRotator)}.", this);

        if (_outcomeSelector == null)
            Debug.LogError($"{nameof(SpinController)}: outcomeSelectorSource does not implement {nameof(IOutcomeSelector)}.", this);
#endif
    }

    private void AutoAssignInterface<TInterface>(ref MonoBehaviour source)
        where TInterface : class
    {
        if (source != null && source is TInterface)
            return;

        var components = GetComponentsInChildren<MonoBehaviour>(true);
        foreach (var comp in components)
        {
            if (comp is TInterface)
            {
                source = comp;
                break;
            }
        }
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
}
