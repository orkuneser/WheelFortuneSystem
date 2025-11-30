using UnityEngine;

public class SpinConfigResolver : MonoBehaviour
{
    [SerializeField] private ZoneSystem _zoneSystem;
    [SerializeField] private SpinConfig _normalConfig;
    [SerializeField] private SpinConfig _safeConfig;
    [SerializeField] private SpinConfig _superConfig;

    public SpinConfig CurrentConfig => ResolveConfig(_zoneSystem.CurrentZone);

    public SpinConfig ResolveConfig(int zone)
    {
        if (zone <= 0)
            zone = 1;

        if (zone % 30 == 0)
            return _superConfig;

        if (zone % 5 == 0)
            return _safeConfig;

        return _normalConfig;
    }
}
