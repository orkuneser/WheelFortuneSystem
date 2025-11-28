public struct RewardEarnedEvent : IGameEvent
{
    public int Amount;

    public RewardEarnedEvent(int amount)
    {
        Amount = amount;
    }
}
