using UnityEngine;
using Sirenix.OdinInspector;

public class ZoneController : Singleton<ZoneController>
{
    [Title("Zone Progress Bar Config")]
    public ZoneProgressBarConfig ProgressBarConfig;

    [Title("Start Zone")]
    [SerializeField] private int _startZone = 1;

    [Title("Debug")]
    [ShowInInspector, ReadOnly] public int CurrentZone { get; private set; }
    [ShowInInspector, ReadOnly] public bool IsSafeZone => CurrentZone % 5 == 0;
    [ShowInInspector, ReadOnly] public bool IsSuperZone => CurrentZone % 30 == 0;

    private void Awake()
    {
        CurrentZone = _startZone;
    }

    public void NextZone()
    {
        CurrentZone++;
        EventManager.Raise(new ZoneChangedEvent(CurrentZone));
    }

    public Color GetColorByZone(int number)
    {
        if (ProgressBarConfig == null)
            return Color.white;

        if (number % 30 == 0)
            return ProgressBarConfig.SafeZoneColor;

        if (number % 5 == 0)
            return ProgressBarConfig.SuperZoneColor;

        return ProgressBarConfig.NormalZoneColor;
    }
}
