using CollectCreatures;
using UnityEngine;


namespace CollectCreatures
{
    public interface IEffect 
    {
        #region FUNC
        void Init(Entity original, Entity target);
        void ApplyEffect();
        #endregion
    }
}
