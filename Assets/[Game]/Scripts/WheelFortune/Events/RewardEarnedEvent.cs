public struct RewardEarnedEvent : IGameEvent
{
    public SpinSlotItemConfig Config;
    public int Amount;

    public RewardEarnedEvent(SpinSlotItemConfig config, int amount)
    {
        Config = config;
        Amount = amount;
    }
}