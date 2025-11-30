using UnityEngine;
using TMPro;

public class ZoneInfoView : BaseEventListener<ZoneChangedEvent>
{
    [SerializeField] private TextMeshProUGUI _safeZoneText;
    [SerializeField] private TextMeshProUGUI _superZoneText;

    private ZoneController _zone => ZoneController.Instance;

    private void Start()
    {
        if (_zone == null)
            return;

        Refresh(_zone.CurrentZone);
    }

    protected override void OnEvent(ZoneChangedEvent changedEvent)
    {
        Refresh(changedEvent.Zone);
    }

    private void Refresh(int currentZone)
    {
        if (_zone == null)
            return;

        int fromZone = currentZone + 1;

        if (_safeZoneText != null)
        {
            int nextSafe = _zone.GetNextSafeZone(fromZone);
            _safeZoneText.SetText(nextSafe.ToString());
        }

        if (_superZoneText != null)
        {
            int nextSuper = _zone.GetNextSuperZone(fromZone);
            _superZoneText.SetText(nextSuper.ToString());
        }
    }
}
