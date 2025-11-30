public struct SpinSlotsUpdatedEvent : IGameEvent
{
    public SpinSlotItemConfig[] Slots;
    public SpinSlotsUpdatedEvent(SpinSlotItemConfig[] s) => Slots = s;
}
