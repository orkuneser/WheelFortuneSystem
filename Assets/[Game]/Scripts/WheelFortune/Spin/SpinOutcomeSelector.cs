using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpinOutcomeSelector : MonoBehaviour, IOutcomeSelector
{
    [SerializeField] private SliceGroupConfig _sliceGroupConfig;
    [SerializeField] private ZoneSystem zoneSystem;

    private SliceConfig _selectedSlice;
    private float _anglePerSlice;

    private void Awake()
    {
        int count = _sliceGroupConfig.SliceConfigs.Length;
        _anglePerSlice = 360f / count;
    }

    public float GenerateTargetAngle()
    {
        var slices = _sliceGroupConfig.SliceConfigs;

        // SAFE ZONE → Bomb yok
        if (zoneSystem.IsSafeZone || zoneSystem.IsSuperZone)
        {
            slices = FilterBombs(slices);
        }

        // Random slice seç
        _selectedSlice = slices[Random.Range(0, slices.Length)];

        int index = GetSliceIndex(_selectedSlice);
        float angle = CalculateAngleForSlice(index);

        return angle;
    }

    public void ResolveOutcome(float finalAngle)
    {
        if (_selectedSlice == null)
            return;

        if (_selectedSlice.IsBomb)
        {
            EventManager.Raise(new BombHitEvent());
            zoneSystem.NextZone();
            return;
        }

        EventManager.Raise(new RewardEarnedEvent(_selectedSlice.RewardAmount));
        zoneSystem.NextZone();
    }

    private int GetSliceIndex(SliceConfig slice)
    {
        for (int i = 0; i < _sliceGroupConfig.SliceConfigs.Length; i++)
        {
            if (_sliceGroupConfig.SliceConfigs[i] == slice)
                return i;
        }
        return 0;
    }

    private float CalculateAngleForSlice(int index)
    {
        // Örn: index 3 → çarkı 3. dilime çevir
        return -index * _anglePerSlice + 2160f; // 6 tur + offset
    }

    private SliceConfig[] FilterBombs(SliceConfig[] arr)
    {
        return Array.FindAll(arr, s => !s.IsBomb);
    }
}
