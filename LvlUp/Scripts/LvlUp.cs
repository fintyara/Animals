using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{
    [CreateAssetMenu(menuName = "LvlUp/LvlUp")]
    public class LvlUp : ScriptableObject
    {
        public List<AnimalType> AnimalTypes => _animalTypes;
        public int NeedExp => _needExp;
        
        [SerializeField] private List<AnimalType> _animalTypes;
        [Space(10)]
        [SerializeField] private int _needExp;
    }
}
