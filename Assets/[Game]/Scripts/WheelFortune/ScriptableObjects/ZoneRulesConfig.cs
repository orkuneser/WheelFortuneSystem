using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Scriptable Objects/Zone Rules Config")]
public class ZoneRulesConfig : ScriptableObject
{
    [Title("Rules")]
    public int SafeZoneInterval = 5;
    public int SuperZoneInterval = 30;

    public bool IsSafeZone(int zone) => zone > 0 && zone % SafeZoneInterval == 0;
    public bool IsSuperZone(int zone) => zone > 0 && zone % SuperZoneInterval == 0;
}