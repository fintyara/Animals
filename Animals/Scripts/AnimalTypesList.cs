using System;
using System.Collections.Generic;
using UnityEngine;

namespace CollectCreatures
{
    [CreateAssetMenu(menuName = "Animals/AnimalTypesList")]
    public class AnimalTypesList : ScriptableObject
    {
        #region VAR
        public event Action onChanged;
        public List<AnimalType> AnimalTypes => _animalTypes;
        [SerializeField] private List<AnimalType> _animalTypes;
        #endregion

        #region FUNC
        public void AddAnimalType(AnimalType animalType)
        {
            if (!_animalTypes.Contains(animalType))
            {
                _animalTypes.Add(animalType);
                onChanged?.Invoke();
            }
        }
        public void ReplaceAnimalTypes(List<AnimalType> animalTyps)
        {
            _animalTypes.Clear();

            for (int i = 0; i < animalTyps.Count; i++)
            {
                _animalTypes.Add(animalTyps[i]);
            }
        }
        #endregion
        
    }
}