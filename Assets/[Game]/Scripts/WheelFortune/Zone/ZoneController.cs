using UnityEngine;
using Sirenix.OdinInspector;

public class ZoneController : Singleton<ZoneController>
{
    [Title("Configs")]
    public ZoneProgressBarConfig ProgressBarConfig;
    public ZoneRulesConfig RulesConfig;

    [Title("Settings")]
    [SerializeField] private int _startZone = 1;

    public int CurrentZone { get; private set; }

    public bool IsSafeZone => RulesConfig != null && RulesConfig.IsSafeZone(CurrentZone);
    public bool IsSuperZone => RulesConfig != null && RulesConfig.IsSuperZone(CurrentZone);

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
        if (ProgressBarConfig == null || RulesConfig == null) return Color.white;
        if (RulesConfig.IsSuperZone(number)) return ProgressBarConfig.SuperZoneColor;
        if (RulesConfig.IsSafeZone(number)) return ProgressBarConfig.SafeZoneColor;
        return ProgressBarConfig.NormalZoneColor;
    }

    public int GetNextSafeZone(int fromZone) => GetNextMultiple(fromZone, RulesConfig != null ? RulesConfig.SafeZoneInterval : 5);
    public int GetNextSuperZone(int fromZone) => GetNextMultiple(fromZone, RulesConfig != null ? RulesConfig.SuperZoneInterval : 30);

    private int GetNextMultiple(int current, int interval)
    {
        if (interval <= 0) return 0;
        int mod = current % interval;
        return mod == 0 ? current : current + (interval - mod);
    }
}