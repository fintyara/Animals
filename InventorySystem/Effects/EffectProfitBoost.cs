using UnityEngine;

[CreateAssetMenu(menuName = "InventorySysem/Effects/EffectProfitBoost")]
public class EffectProfitBoost : ItemEffect
{
    public float factor = 1.2f;

    public override void Use()
    {
        Debug.Log("Profit boosting in " + factor);
    }
}
