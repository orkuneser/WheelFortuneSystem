using UnityEngine;
using Sirenix.OdinInspector;

public class ZoneSystem : Singleton<ZoneSystem>
{
    [SerializeField] private int startZone = 1;

    [ShowInInspector, ReadOnly] public int CurrentZone { get; private set; }

    public bool IsSafeZone => CurrentZone % 5 == 0;
    public bool IsSuperZone => CurrentZone % 30 == 0;

    private void Awake()
    {
        CurrentZone = startZone;
    }

    public void NextZone()
    {
        CurrentZone++;
        EventManager.Raise(new ZoneChangedEvent(CurrentZone));
    }
}
