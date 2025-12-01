using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UiSpinButton : BaseMultiEventListener
{
    [SerializeField] private Button _button;
    [SerializeField] private SpinController _spinController;

    private void OnValidate()
    {
        if (_button == null)
            _button = GetComponent<Button>();

        if (_spinController == null)
            _spinController = FindAnyObjectByType<SpinController>();
    }

    private void OnEnable()
    {
        if (_button == null || _spinController == null)
            return;

        _button.onClick.AddListener(OnClicked);

        AddHandler<SpinStartedEvent>(OnSpinStarted);
        AddHandler<SpinSlotsUpdatedEvent>(OnSpinSlotsUpdated);
    }

    private void OnClicked()
    {
        _spinController.TryStartSpin();
    }

    private void OnSpinStarted(SpinStartedEvent startedEvent)
    {
        UpdateInteractable(false);
    }

    private void OnSpinSlotsUpdated(SpinSlotsUpdatedEvent updatedEvent)
    {
        UpdateInteractable(true);
    }

    private void UpdateInteractable(bool interactable)
    {
        _button.interactable = interactable;
    }
}