public interface ISpinRotator
{
    bool IsSpinning { get; }
    void RotateTo(float targetAngle, float duration);
}
