using UnityEngine;
using Sirenix.OdinInspector;

public class SpinController : BaseMultiEventListener
{
    [Title("Spin Refs")]
    [SerializeField] private MonoBehaviour _rotatorSource;          // ISpinRotator
    [SerializeField] private MonoBehaviour _outcomeSelectorSource;  // IOutcomeSelector

    [Title("Settings")]
    [SerializeField] private float _spinDuration;

    private ISpinRotator _rotator;
    private IOutcomeSelector _outcomeSelector;

    private void OnValidate()
    {
        if (_rotatorSource == null)
            _rotatorSource = GetComponentInChildren<ISpinRotator>() as MonoBehaviour;
        if (_outcomeSelectorSource == null)
            _outcomeSelectorSource = GetComponentInChildren<IOutcomeSelector>() as MonoBehaviour;
    }

    private void Awake()
    {
        CacheInterfaces();
    }

    private void OnEnable()
    {
        AddHandler<SpinCompletedEvent>(OnSpinCompleted);
        AddHandler<RewardAnimationCompletedEvent>(OnRewardAnimationFinished);
    }

    private void CacheInterfaces()
    {
        if (_rotatorSource is ISpinRotator rotator) _rotator = rotator;
        if (_outcomeSelectorSource is IOutcomeSelector selector) _outcomeSelector = selector;
    }

    [Button]
    public void TryStartSpin()
    {
        if (_rotator == null || _outcomeSelector == null) CacheInterfaces();
        if (_rotator == null || _outcomeSelector == null || _rotator.IsSpinning) return;

        EventManager.Raise(new SpinStartedEvent());
        float angle = _outcomeSelector.GenerateTargetAngle();
        _rotator.RotateTo(angle, _spinDuration);
    }

    private void OnSpinCompleted(SpinCompletedEvent completedEvent)
    {
        if (_outcomeSelector == null) CacheInterfaces();
        _outcomeSelector?.ResolveOutcome(completedEvent.FinalAngle);
    }

    private void OnRewardAnimationFinished(RewardAnimationCompletedEvent evt)
    {
        if (ZoneController.Instance != null)
        {
            ZoneController.Instance.NextZone();
        }
    }
}