using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Spin/Spin Config")]
public class SpinConfig : ScriptableObject
{
    [Title("VISUAL CONFIG")]
    [Title("Spin"), InlineEditor(InlineEditorModes.LargePreview)]
    public Sprite SpinSprite;
    [Title("Indicator"), InlineEditor(InlineEditorModes.LargePreview)]
    public Sprite SpinIndicatorSprite;
    [Title("Text Color")]
    public Color DisplayTextColor;
    [Title("Spin Name")]
    public string SpinName;

    [Title("Slot Items")]
    public SpinSlotItemConfig[] SlotItemConfigs;
}
