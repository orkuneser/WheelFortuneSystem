using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Spin/Slice Config")]
public class SliceConfig : ScriptableObject
{
    public string Id;
    public Sprite Icon;
    public bool IsBomb;
    public int RewardAmount;
}