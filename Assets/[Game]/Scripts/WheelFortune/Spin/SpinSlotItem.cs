using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class SpinSlotItem : BaseMultiEventListener
{
    [Title("Collider")]
    [SerializeField] private BoxCollider2D _collider;

    [Title("Visuals")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private TMP_Text _rewardText;

    private SpinSlotItemConfig _config;

    private void OnValidate()
    {
        if (_collider == null)
            _collider = GetComponentInChildren<BoxCollider2D>();
    }

    private void OnEnable()
    {
        AddHandler<SpinStartedEvent>(OnSpinStarted);
        AddHandler<SpinCompletedEvent>(OnSpinCompleted);
    }

    public void Init(SpinSlotItemConfig config, int displayAmount)
    {
        _config = config;

        if (_iconImage != null)
            _iconImage.sprite = config.Icon;

        if (_rewardText != null)
        {
            if (config.IsBomb)
                _rewardText.text = config.ItemName;
            else
                _rewardText.text = "X" + displayAmount;
        }
    }

    private void OnSpinStarted(SpinStartedEvent startedEvent)
    {
        if (_collider != null)
            _collider.enabled = true;
    }

    private void OnSpinCompleted(SpinCompletedEvent completedEvent)
    {
        if (_collider != null)
            _collider.enabled = false;
    }
}
