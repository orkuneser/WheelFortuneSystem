using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class UiRewardCardPanel : UiPanel
{
    [Title("References")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Transform _visualContainer;

    [Title("Animation Settings")]
    [SerializeField] private float _animationDuration = 0.5f;

    public Transform IconTransform => _iconImage != null ? _iconImage.transform : transform;

    public void Setup(SpinSlotItemConfig config, int amount)
    {
        if (config == null) return;

        if (_iconImage) _iconImage.sprite = config.Icon;
        if (_nameText) _nameText.text = config.DisplayName;
        if (_amountText) _amountText.text = "x" + ScoreFormatter.FormatF0(amount);
    }

    public override void Show()
    {
        if (IsVisible) return;
        base.Show();

        _visualContainer.localScale = Vector3.zero;
        _visualContainer.localRotation = Quaternion.Euler(0, 0, -180f);

        Sequence seq = DOTween.Sequence();
        seq.Append(_visualContainer.DOScale(Vector3.one, _animationDuration)
           .SetEase(Ease.OutBack));
        seq.Join(_visualContainer.DOLocalRotate(Vector3.zero, _animationDuration, RotateMode.FastBeyond360)
           .SetEase(Ease.OutBack));
    }

    public override void Hide()
    {
        if (!IsVisible) return;

        _visualContainer.DOScale(Vector3.zero, _animationDuration * 0.8f)
            .SetEase(Ease.InBack)
            .OnComplete(() => base.Hide());
    }
}