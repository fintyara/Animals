using UnityEngine;

[CreateAssetMenu(menuName = "InventorySysem/Effects/EffectSpeedBoost")]
public class EffectSpeedBoost : ItemEffect
{
    public int amount = 2;

    public override void Use()
    {
        Debug.Log("speed amount " + amount);
    }
}
