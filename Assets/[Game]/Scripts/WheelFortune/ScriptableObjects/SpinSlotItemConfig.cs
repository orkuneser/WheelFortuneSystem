using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Scriptable Objects/Spin Slot Item Config")]
public class SpinSlotItemConfig : ScriptableObject
{
    [Title("CONFIGURATION")]
    [EnumToggleButtons]
    public RewardType Type;

    [Title("VISUALS"), InlineEditor(InlineEditorModes.LargePreview)]
    public Sprite Icon;

    [Title("DATA")]
    public string DisplayName;
    public int RewardAmount;
}