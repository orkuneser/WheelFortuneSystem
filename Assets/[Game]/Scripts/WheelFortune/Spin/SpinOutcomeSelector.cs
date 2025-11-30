using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpinOutcomeSelector : MonoBehaviour, IOutcomeSelector
{
    [SerializeField] private SliceGroupConfig _sliceGroupConfig;
    [SerializeField] private ZoneSystem zoneSystem;
    [SerializeField] private int baseFullRotations = 6;

    private float _anglePerSlice;
    private float _extraRotationDegrees;

    private void Awake()
    {
        int count = _sliceGroupConfig.SliceConfigs.Length;
        _anglePerSlice = 360f / Mathf.Max(1, count);
        _extraRotationDegrees = 360f * Mathf.Max(1, baseFullRotations);
    }

    public float GenerateTargetAngle()
    {
        var allSlices = _sliceGroupConfig.SliceConfigs;
        var selectable = allSlices;

        if (zoneSystem.IsSafeZone || zoneSystem.IsSuperZone)
        {
            selectable = FilterBombs(allSlices);
        }

        if (selectable == null || selectable.Length == 0)
        {
            Debug.LogError($"{nameof(SpinOutcomeSelector)}: No selectable slices configured.");
            return _extraRotationDegrees;
        }

        var chosen = selectable[Random.Range(0, selectable.Length)];

        int index = GetSliceIndex(chosen);
        if (index < 0)
        {
            Debug.LogError($"{nameof(SpinOutcomeSelector)}: Chosen slice not found in SliceGroupConfig.");
            index = 0;
        }

        return CalculateAngleForSlice(index);
    }

    public void ResolveOutcome(float finalAngle)
    {
        var allSlices = _sliceGroupConfig.SliceConfigs;
        if (allSlices == null || allSlices.Length == 0)
            return;

        int sliceCount = allSlices.Length;
        int index = GetIndexFromAngle(finalAngle, sliceCount);
        if (index < 0 || index >= sliceCount)
            return;

        var slice = allSlices[index];

        if (slice.IsBomb)
        {
            EventManager.Raise(new BombHitEvent());
            zoneSystem.NextZone();
            return;
        }

        int amount = CalculateZoneScaledReward(slice.RewardAmount);
        EventManager.Raise(new RewardEarnedEvent(amount));

        zoneSystem.NextZone();
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
        int zone = Mathf.Max(1, zoneSystem.CurrentZone);

        int multiplier = zone;

        if (zoneSystem.IsSuperZone)
            multiplier *= 2;

        return baseAmount * multiplier;
    }

    private SpinSlotItemConfig[] FilterBombs(SpinSlotItemConfig[] arr)
    {
        return Array.FindAll(arr, s => !s.IsBomb);
    }

    private int GetSliceIndex(SpinSlotItemConfig slice)
    {
        var arr = _sliceGroupConfig.SliceConfigs;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == slice)
                return i;
        }

        return -1;
    }
}
