public struct ZoneChangedEvent : IGameEvent
{
    public int Zone;

    public ZoneChangedEvent(int zone)
    {
        Zone = zone;
    }
}
