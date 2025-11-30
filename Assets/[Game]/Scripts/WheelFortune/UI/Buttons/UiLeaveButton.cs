using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UiLeaveButton : BaseMultiEventListener
{
    [SerializeField] private Button _button;

    private void OnValidate()
    {
        if (_button == null)
            _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        AddHandler<SpinStartedEvent>(OnSpinStarted);
        AddHandler<ZoneChangedEvent>(OnZoneChanged);
    }

    private void Start()
    {
        CheckZoneForInteractable();
    }

    private void OnSpinStarted(SpinStartedEvent startedEvent) => UpdateInteractable(false);
    private void OnZoneChanged(ZoneChangedEvent zoneChangedEvent) => CheckZoneForInteractable();

    private void UpdateInteractable(bool interactable)
    {
        _button.interactable = interactable;
    }

    private void CheckZoneForInteractable()
    {
        ZoneSystem zone = ZoneSystem.Instance;
        if (zone.IsSafeZone || zone.IsSuperZone)
        {
            UpdateInteractable(true);
        }
        else
            UpdateInteractable(false);
    }
}
