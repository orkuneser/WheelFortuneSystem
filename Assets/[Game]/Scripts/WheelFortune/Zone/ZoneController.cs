using UnityEngine;
using Sirenix.OdinInspector;

public class ZoneController : Singleton<ZoneController>
{
    [Title("Zone Progress Bar Config")]
    public ZoneProgressBarConfig ProgressBarConfig;

    [Title("Zone Settings")]
    [SerializeField] private int _safeZoneInterval = 5;
    [SerializeField] private int _superZoneInterval = 30;

    [Title("Start Zone")]
    [SerializeField] private int _startZone = 1;

    [Title("Debug")]
    [ShowInInspector, ReadOnly] public int CurrentZone { get; private set; }
    [ShowInInspector, ReadOnly] public bool IsSafeZone => CurrentZone % _safeZoneInterval == 0;
    [ShowInInspector, ReadOnly] public bool IsSuperZone => CurrentZone % _superZoneInterval == 0;

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

        if (number % _superZoneInterval == 0)
            return ProgressBarConfig.SuperZoneColor;

        if (number % _safeZoneInterval == 0)
            return ProgressBarConfig.SafeZoneColor;

        return ProgressBarConfig.NormalZoneColor;
    }

    public int GetNextSafeZone(int fromZone)
    {
        return GetNextMultiple(fromZone, _safeZoneInterval);
    }

    public int GetNextSuperZone(int fromZone)
    {
        return GetNextMultiple(fromZone, _superZoneInterval);
    }

    private int GetNextMultiple(int current, int interval)
    {
        if (interval <= 0)
            return 0;

        int mod = current % interval;
        return mod == 0 ? current : current + (interval - mod);
    }
}
