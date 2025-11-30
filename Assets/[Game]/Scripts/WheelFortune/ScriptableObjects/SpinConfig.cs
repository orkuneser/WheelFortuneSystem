using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Spin/Spin Config")]
public class SpinConfig : ScriptableObject
{
    [Title("VISUAL CONFIG")]
    [Title("Spin"), InlineEditor(InlineEditorModes.LargePreview)]
    [SerializeField] private Sprite SpinSprite;
    [Title("Indicator"), InlineEditor(InlineEditorModes.LargePreview)]
    [SerializeField] private Sprite SpinIndicatorSprite;
    [Title("Text Color")]
    [SerializeField] private Color DisplayTextColor;

    [Title("Slot Items")]
    public SpinSlotItemConfig[] SlotItemConfigs;
}
