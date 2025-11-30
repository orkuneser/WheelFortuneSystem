using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpinOutcomeSelector : BaseMultiEventListener, IOutcomeSelector
{
    [SerializeField] private int baseFullRotations = 6;

    private SpinSlotItemConfig[] _currentSlots = Array.Empty<SpinSlotItemConfig>();
    private float _anglePerSlice = 360f;
    private float _extraRotationDegrees;

    private void Awake()
    {
        _extraRotationDegrees = 360f * Mathf.Max(1, baseFullRotations);
    }

    private void OnEnable()
    {
        AddHandler<SpinSlotsUpdatedEvent>(OnSpinSlotsUpdated);
    }

    private void OnSpinSlotsUpdated(SpinSlotsUpdatedEvent spinSlotsUpdated)
    {
        _currentSlots = spinSlotsUpdated.Slots ?? Array.Empty<SpinSlotItemConfig>();

        int count = Mathf.Max(1, _currentSlots.Length);
        _anglePerSlice = 360f / count;
    }

    public float GenerateTargetAngle()
    {
        if (_currentSlots == null || _currentSlots.Length == 0)
            return _extraRotationDegrees;

        var chosen = _currentSlots[Random.Range(0, _currentSlots.Length)];
        int index = Array.IndexOf(_currentSlots, chosen);
        if (index < 0)
            index = 0;

        return CalculateAngleForSlice(index);
    }

    public void ResolveOutcome(float finalAngle)
    {
        if (_currentSlots == null || _currentSlots.Length == 0)
            return;

        int sliceCount = _currentSlots.Length;
        int index = GetIndexFromAngle(finalAngle, sliceCount);
        if (index < 0 || index >= sliceCount)
            return;

        var slot = _currentSlots[index];
        if (slot == null)
            return;

        if (slot.IsBomb)
        {
            EventManager.Raise(new BombHitEvent());
            ZoneController.Instance.NextZone();
            return;
        }

        int amount = CalculateZoneScaledReward(slot.RewardAmount);
        EventManager.Raise(new RewardEarnedEvent(amount));

        ZoneController.Instance.NextZone();
    }

    private int GetIndexFromAngle(float finalAngle, int sliceCount)
    {
        float rawIndex = (_extraRotationDegrees - finalAngle) / _anglePerSlice;
        int index = Mathf.RoundToInt(rawIndex);

        index %= sliceCount;
        if (index < 0)
            index += sliceCount;

        return index;
    }

    private float CalculateAngleForSlice(int index)
    {
        return -index * _anglePerSlice + _extraRotationDegrees;
    }

    private int CalculateZoneScaledReward(int baseAmount)
    {
        int zone = Mathf.Max(1, ZoneController.Instance.CurrentZone);

        int multiplier = zone;
        if (ZoneController.Instance.IsSuperZone)
            multiplier *= 2;

        return baseAmount * multiplier;
    }
}
