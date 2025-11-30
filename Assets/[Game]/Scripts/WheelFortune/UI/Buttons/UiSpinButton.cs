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

    private void Awake()
    {
        if (_button == null || _spinController == null)
            return;

        _button.onClick.AddListener(OnClicked);
    }

    private void OnEnable()
    {
        AddHandler<SpinStartedEvent>(OnSpinStarted);
        AddHandler<SpinCompletedEvent>(OnSpinCompleted);
    }

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    {
        _spinController.TryStartSpin();
    }

    private void OnSpinStarted(SpinStartedEvent startedEvent) => UpdateInteractable(false);
    private void OnSpinCompleted(SpinCompletedEvent completedEvent) => UpdateInteractable(true);

    private void UpdateInteractable(bool interactable)
    {
        _button.interactable = interactable;
    }
}
