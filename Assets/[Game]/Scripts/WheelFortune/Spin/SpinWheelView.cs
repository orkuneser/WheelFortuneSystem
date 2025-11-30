using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheelView : BaseMultiEventListener
{
    [Title("Core Refs")]
    [SerializeField] private SpinConfigResolver _configResolver;

    [Title("Visual Refs")]
    [SerializeField] private TextMeshProUGUI _spinTypeText;
    [SerializeField] private Image _spinImage;
    [SerializeField] private Image _indicatorImage;
    [SerializeField] private SpinSlotItem[] _slotItems;

    private SpinSlotItemConfig[] _shuffledSlots = Array.Empty<SpinSlotItemConfig>();

    private void OnEnable()
    {
        AddHandler<ZoneChangedEvent>(OnZoneChanged);
    }

    private void Start()
    {
        RefreshWheel();
    }

    private void OnZoneChanged(ZoneChangedEvent evt)
    {
        RefreshWheel();
    }

    private void RefreshWheel()
    {
        var cfg = _configResolver != null ? _configResolver.CurrentConfig : null;

        if (cfg == null)
        {
            _shuffledSlots = Array.Empty<SpinSlotItemConfig>();
            Debug.LogError("[SpinWheelView] SpinConfigResolver.CurrentConfig null, slotlar boş.");
            EventManager.Raise(new SpinSlotsUpdatedEvent(_shuffledSlots));
            return;
        }

        if (_spinImage != null)
            _spinImage.sprite = cfg.SpinSprite;

        if (_indicatorImage != null)
            _indicatorImage.sprite = cfg.SpinIndicatorSprite;

        var slots = cfg.SlotItemConfigs ?? Array.Empty<SpinSlotItemConfig>();
        _shuffledSlots = Shuffle(slots);

        int count = Mathf.Min(_slotItems.Length, _shuffledSlots.Length);

        for (int i = 0; i < _slotItems.Length; i++)
        {
            var view = _slotItems[i];
            if (view == null)
                continue;

            if (i < count)
            {
                var itemCfg = _shuffledSlots[i];
                int displayAmount = (itemCfg != null && !itemCfg.IsBomb)
                    ? itemCfg.RewardAmount
                    : 0;

                view.Init(itemCfg, displayAmount);
                view.gameObject.SetActive(true);
            }
            else
            {
                view.Init(null, 0);
                view.gameObject.SetActive(false);
            }
        }

        if (_spinTypeText != null)
        {
            _spinTypeText.SetText(cfg.SpinName);
            _spinTypeText.color = cfg.SpinTypeTextColor;
        }

        EventManager.Raise(new SpinSlotsUpdatedEvent(_shuffledSlots));
    }

    private static SpinSlotItemConfig[] Shuffle(SpinSlotItemConfig[] source)
    {
        int length = source?.Length ?? 0;
        if (length == 0)
            return Array.Empty<SpinSlotItemConfig>();

        var arr = (SpinSlotItemConfig[])source.Clone();

        for (int i = arr.Length - 1; i > 0; i--)
        {
            int r = UnityEngine.Random.Range(0, i + 1);
            (arr[i], arr[r]) = (arr[r], arr[i]);
        }

        return arr;
    }
}
