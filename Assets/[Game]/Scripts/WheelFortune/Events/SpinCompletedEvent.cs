public struct SpinCompletedEvent : IGameEvent
{
    public float FinalAngle;

    public SpinCompletedEvent(float finalAngle)
    {
        FinalAngle = finalAngle;
    }
}
