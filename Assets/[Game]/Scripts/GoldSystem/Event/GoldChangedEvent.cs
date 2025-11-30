public struct GoldChangedEvent : IGameEvent
{
    public int NewAmount { get; }

    public GoldChangedEvent(int newAmount)
    {
        NewAmount = newAmount;
    }
}
