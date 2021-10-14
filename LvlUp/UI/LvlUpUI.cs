using System.Collections;
using System.Collections.Generic;
using CollectCreatures;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CollectCreatures.UI
{
    public class LvlUpUI : MonoBehaviour
    {
        #region VAR
        [SerializeField] private GameObject _panel;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private TMP_Text _lvlText;
        [SerializeField] private Image _ico;
        
        [Space(10)]
        [SerializeField] private float timePrepare;
        [SerializeField] private float timeDelaySound;
        private int _curLvl;
        private List<AnimalType> _unlockedAnimalTypes = new List<AnimalType>();
        
        #endregion

        #region FUNC
        private IEnumerator ShowLvlUpRoutine()
        {
            yield return new WaitForSeconds(timePrepare);
            _panel.SetActive(true);
            yield return new WaitForSeconds(timeDelaySound);
            if(_audioSource != null)
                _audioSource.Play();
        }
        #endregion   
        
        #region CALLBAKS     
        // V Code referenced by UnityEvents only V   
        public void PressOk()
        {
            _panel.SetActive(false);
        }
        public virtual void UpdateLvl(int lvl)
        {
            _curLvl = lvl;
        
        }  
        public virtual void UnlockedAnimals(List<AnimalType> animalTypes)
        {
            _unlockedAnimalTypes = animalTypes;
            _lvlText.text = _curLvl.ToString();

            if (_unlockedAnimalTypes.Count > 0)
            {
                _ico.sprite = _unlockedAnimalTypes[0].Ico;
            }

            StartCoroutine(ShowLvlUpRoutine());
        }
        #endregion
    }
}
