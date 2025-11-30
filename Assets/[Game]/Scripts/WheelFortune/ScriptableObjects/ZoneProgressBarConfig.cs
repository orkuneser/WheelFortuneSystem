using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Spin/Zone Progress Bar Config")]
public class ZoneProgressBarConfig : ScriptableObject
{
    [Title("Past")]
    public Color NormalZoneColor;

    [Title("Current")]
    public Color SafeZoneColor;

    [Title("Future")]
    public Color SuperZoneColor;
}
