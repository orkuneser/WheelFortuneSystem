using System;

public static class SpinContentLogic
{
    public static SpinSlotItemConfig[] GenerateRoundSlots(SpinConfig config, int slotCount)
    {
        if (config == null || config.SlotItemConfigs == null)
            return Array.Empty<SpinSlotItemConfig>();

        var allItems = config.SlotItemConfigs;

        if (ZoneController.Instance.IsSafeZone || ZoneController.Instance.IsSuperZone)
        {
            return GetRandomSubset(allItems, slotCount);
        }

        return GenerateNormalZoneSlots(allItems, slotCount);
    }

    private static SpinSlotItemConfig[] GenerateNormalZoneSlots(SpinSlotItemConfig[] allItems, int count)
    {
        var bombs = Array.FindAll(allItems, x => x.Type == RewardType.Bomb);
        var others = Array.FindAll(allItems, x => x.Type != RewardType.Bomb);

        if (bombs.Length == 0 || others.Length == 0)
            return GetRandomSubset(allItems, count);

        var result = new SpinSlotItemConfig[count];

        result[0] = bombs[0];

        var shuffledOthers = others.ShuffledCopy();
        for (int i = 1; i < count; i++)
        {
            result[i] = shuffledOthers[(i - 1) % shuffledOthers.Length];
        }

        return result.ShuffledCopy();
    }

    private static SpinSlotItemConfig[] GetRandomSubset(SpinSlotItemConfig[] source, int count)
    {
        var result = new SpinSlotItemConfig[count];
        var shuffled = source.ShuffledCopy();

        if (shuffled.Length == 0) return result;

        for (int i = 0; i < count; i++)
        {
            result[i] = shuffled[i % shuffled.Length];
        }
        return result;
    }
}