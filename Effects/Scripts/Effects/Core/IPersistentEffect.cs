using UnityEngine;


namespace CollectCreatures
{
    public interface  IPersistentEffect : IEffect
    {
        #region FUNC
        void Clear();
        void Destroy();
        #endregion

    }
}
