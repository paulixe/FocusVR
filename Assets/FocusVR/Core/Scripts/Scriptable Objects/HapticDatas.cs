using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableOjects/FocusVR/"+nameof(HapticDatas))]
public class HapticDatas : ScriptableObject
{
    public float duration;
    [Range(0,320)]public float frequency;
    [Range(0,1)]public float amplitude;
}
