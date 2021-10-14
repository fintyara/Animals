using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{
    [CreateAssetMenu(menuName = "LvlUp/AllLvlUp")]
    public class AllLvlUp : ScriptableObject
    {
        public List<LvlUp> lvlUps => _lvlUps;
        [SerializeField] private List<LvlUp> _lvlUps;
    }
}
