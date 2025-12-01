using UnityEngine;

public static class RewardCalculator
{
    public static int CalculateZoneScaledReward(int baseAmount, int currentZone, bool isSuperZone)
    {
        int zoneMultiplier = Mathf.Max(1, currentZone);

        if (isSuperZone)
            zoneMultiplier *= 2;

        return baseAmount * zoneMultiplier;
    }
}