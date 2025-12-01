using UnityEngine;

public class SpinConfigResolver : MonoBehaviour
{
    [SerializeField] private SpinConfig _normalConfig;
    [SerializeField] private SpinConfig _safeConfig;
    [SerializeField] private SpinConfig _superConfig;

    public SpinConfig CurrentConfig => ResolveConfig(ZoneController.Instance.CurrentZone);

    public SpinConfig ResolveConfig(int zone)
    {
        if (ZoneController.Instance.IsSuperZone) return _superConfig;
        if (ZoneController.Instance.IsSafeZone) return _safeConfig;
        return _normalConfig;
    }
}