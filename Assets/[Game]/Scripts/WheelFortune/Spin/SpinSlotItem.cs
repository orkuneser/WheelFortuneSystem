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

    public void Init(SpinSlotItemConfig config)
    {
        if (config == null) return;

        _iconImage.sprite = config.Icon;

        if (config.Type == RewardType.Bomb)
            _rewardText.text = config.DisplayName;
        else
            _rewardText.text = "x" + ScoreFormatter.FormatF2(config.RewardAmount);
    }

    private void OnSpinStarted(SpinStartedEvent startedEvent)
    {
        if (_collider != null) _collider.enabled = true;
    }

    private void OnSpinCompleted(SpinCompletedEvent completedEvent)
    {
        if (_collider != null) _collider.enabled = false;
    }
}