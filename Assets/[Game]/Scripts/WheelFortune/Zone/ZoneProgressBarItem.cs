using UnityEngine;
using TMPro;

public class ZoneProgressBarItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnValidate()
    {
        if (_text == null)
            _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(int number)
    {
        _text.SetText(ScoreFormatter.FormatF0(number));
        _text.color = ZoneController.Instance.GetColorByZone(number);
    }
}
