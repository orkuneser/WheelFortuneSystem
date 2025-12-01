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

    private SpinSlotItemConfig[] _currentRoundSlots = Array.Empty<SpinSlotItemConfig>();

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
            HandleEmptyState();
            return;
        }

        UpdateStaticVisuals(cfg);

        var allPossibleItems = cfg.SlotItemConfigs ?? Array.Empty<SpinSlotItemConfig>();

        if (allPossibleItems.Length == 0)
        {
            HandleEmptyState();
            return;
        }

        var shuffledPool = allPossibleItems.ShuffledCopy();

        int visualSlotCount = _slotItems.Length;
        _currentRoundSlots = new SpinSlotItemConfig[visualSlotCount];

        for (int i = 0; i < visualSlotCount; i++)
        {
            _currentRoundSlots[i] = shuffledPool[i % shuffledPool.Length];
        }

        UpdateSlotItems(_currentRoundSlots);
        EventManager.Raise(new SpinSlotsUpdatedEvent(_currentRoundSlots));
    }

    private void HandleEmptyState()
    {
        _currentRoundSlots = Array.Empty<SpinSlotItemConfig>();
        EventManager.Raise(new SpinSlotsUpdatedEvent(_currentRoundSlots));
    }

    private void UpdateStaticVisuals(SpinConfig cfg)
    {
        if (_spinImage != null) _spinImage.sprite = cfg.SpinSprite;
        if (_indicatorImage != null) _indicatorImage.sprite = cfg.SpinIndicatorSprite;

        if (_spinTypeText != null)
        {
            _spinTypeText.SetText(cfg.SpinName);
            _spinTypeText.color = cfg.SpinTypeTextColor;
        }
    }

    private void UpdateSlotItems(SpinSlotItemConfig[] slots)
    {
        int maxSlots = _slotItems.Length;

        for (int i = 0; i < maxSlots; i++)
        {
            var view = _slotItems[i];
            if (view == null) continue;

            if (i < slots.Length)
            {
                view.Init(slots[i]);
            }
        }
    }
}