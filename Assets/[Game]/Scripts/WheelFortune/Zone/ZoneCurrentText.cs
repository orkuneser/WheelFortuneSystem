using UnityEngine;
using TMPro;

public class ZoneCurrentText : BaseEventListener<ZoneChangedEvent>
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnValidate()
    {
        if (_text == null)
            _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Refresh(ZoneController.Instance.CurrentZone);
    }

    protected override void OnEvent(ZoneChangedEvent evt)
    {
        Refresh(evt.Zone);
    }

    private void Refresh(int zone)
    {
        if (_text == null)
            return;

        _text.SetText(zone.ToString());
        _text.color = ZoneController.Instance.GetColorByZone(zone);
    }
}
