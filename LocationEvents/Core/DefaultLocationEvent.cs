using UnityEngine;

namespace CollectCreatures.LocationEvents
{
    public class DefaultLocationEvent : LocationEvent
    {
        #region MONO
        private void Start()
        {
            spawnPoints = GetComponent<ShuffledSpawnPoints>();
            if (!spawnPoints)
                Debug.Log("Need points");
        }
        private void Update()
        {
            UpdateEvent();       
        }
        #endregion
    }
}
