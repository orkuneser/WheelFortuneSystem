using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiCollectedItem : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _amountTextValue;

    public void Setup(SpinSlotItemConfig config, int amount)
    {
        if (_iconImage != null) _iconImage.sprite = config.Icon;
        if (_amountTextValue != null) _amountTextValue.text = "x" + ScoreFormatter.FormatF2(amount);
    }
}