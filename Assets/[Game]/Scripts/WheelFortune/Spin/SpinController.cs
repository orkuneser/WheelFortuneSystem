using UnityEngine;
using Sirenix.OdinInspector;

public class SpinController : BaseEventListener<SpinCompletedEvent>
{
    [SerializeField] private SpinRotator rotatorRef;
    [SerializeField] private SpinOutcomeSelector outcomeSelectorRef;

    [Button]
    public void TryStartSpin()
    {
        if (rotatorRef.IsSpinning)
            return;

        EventManager.Raise(new SpinStartedEvent());

        float angle = outcomeSelectorRef.GenerateTargetAngle();
        rotatorRef.RotateTo(angle, 2f);
    }

    protected override void OnEvent(SpinCompletedEvent evt)
    {
        outcomeSelectorRef.ResolveOutcome(evt.FinalAngle);
    }
}
