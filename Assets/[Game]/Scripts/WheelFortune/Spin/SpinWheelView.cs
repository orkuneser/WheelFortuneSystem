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

    private void OnEnable() => AddHandler<ZoneChangedEvent>(evt => RefreshWheel());
    private void Start() => RefreshWheel();

    private void RefreshWheel()
    {
        var cfg = _configResolver != null ? _configResolver.CurrentConfig : null;

        UpdateStaticVisuals(cfg);
        var roundSlots = SpinContentLogic.GenerateRoundSlots(cfg, _slotItems.Length);

        UpdateSlotItems(roundSlots);
        EventManager.Raise(new SpinSlotsUpdatedEvent(roundSlots));
    }

    private void UpdateStaticVisuals(SpinConfig cfg)
    {
        if (cfg == null) return;
        if (_spinImage) _spinImage.sprite = cfg.SpinSprite;
        if (_indicatorImage) _indicatorImage.sprite = cfg.SpinIndicatorSprite;
        if (_spinTypeText) { _spinTypeText.SetText(cfg.SpinName); _spinTypeText.color = cfg.SpinTypeTextColor; }
    }

    private void UpdateSlotItems(SpinSlotItemConfig[] slots)
    {
        for (int i = 0; i < _slotItems.Length; i++)
        {
            if (_slotItems[i] != null && i < slots.Length)
            {
                _slotItems[i].Init(slots[i]);
            }
        }
    }
}