using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class GoldDisplayText : BaseEventListener<GoldChangedEvent>
{
    [Title("Text Reference")]
    [SerializeField] private TextMeshProUGUI _text;

    private void OnValidate()
    {
        if (_text == null)
            _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (GoldManager.Instance != null)
            UpdateText(GoldManager.Instance.GoldAmount);
    }

    protected override void OnEvent(GoldChangedEvent evt)
    {
        UpdateText(evt.NewAmount);
    }

    private void UpdateText(int amount)
    {
        _text.SetText(ScoreFormatter.FormatF2(amount));
    }
}
