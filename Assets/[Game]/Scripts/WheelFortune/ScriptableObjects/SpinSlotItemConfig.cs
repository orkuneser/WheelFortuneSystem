using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Spin/Spin Slot Item Config")]
public class SpinSlotItemConfig : ScriptableObject
{
    [Title("ICON"), InlineEditor(InlineEditorModes.LargePreview)]
    public Sprite Icon;

    [Title("Item Name")]
    public string ItemName;

    [Title("Item Type")]
    public bool IsBomb;

    [Title("Item Reward Amount")]
    public int RewardAmount;
}