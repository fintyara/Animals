using UnityEngine;
using System.Collections.Generic;

namespace CollectCreatures
{
    [CreateAssetMenu(menuName = "Animals/Animals")]
    public class Animals : ScriptableObject
    {
        public List<AnimalType> AnimalTypes => _animalTypes;
        [SerializeField] private List<AnimalType> _animalTypes;
        
        void OnValidate()
        {
            for (int i = 0; i < _animalTypes.Count; i++)
            {
                _animalTypes[i].id = i;
            }

            //  for (int i = 0; i < AllAnimals.Count; i++)
            //  {
            //      AllAnimals[i].Ico = Resources.Load<Sprite>("Sprites/Icons/Animals/" + AllAnimals[i].name);
            //     AllAnimals[i].Prefab = Resources.Load<GameObject>("Prefabs/Animals/" + "Creature" + AllAnimals[i].name);
            //  }
        }
    }
}

