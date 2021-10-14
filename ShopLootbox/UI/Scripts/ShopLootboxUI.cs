using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CollectCreatures.UI
{
    public class ShopLootboxUI : MonoBehaviour
    {
        #region VAR
        [SerializeField] private GameObject _panel;
        [SerializeField] private float _timeAnimationShowBack;
        [SerializeField] private float _delayStart;
        [SerializeField] private float _delayShowRandom;
        [SerializeField] private float _countChangeRandom;
        [SerializeField] private Image _droppedImage;
        [SerializeField] private Animator _showBackAnimator;
        [SerializeField] private GameObject _TakeButtonGO;
        
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;
        [SerializeField] private AudioClip _lootboxClicked;
        [SerializeField] private AudioClip _randomRepeat;
        [SerializeField] private AudioClip _randomReady;
        [SerializeField] private AudioClip _animalClicked;
        
        private LootboxType _selectedLootbox;
        private List<AnimalItem> possible = new List<AnimalItem>();
        private AnimalType _droppedAnimalType;
        private bool _lootboxActivated;
        private static readonly int Open = Animator.StringToHash("open");
        private static readonly int Close1 = Animator.StringToHash("close");
        #endregion

        #region FUNC
        public void OpenLootbox(LootboxType lootboxType,AnimalType droppedAnimalType)
        {
            _selectedLootbox = lootboxType;
            _droppedAnimalType = droppedAnimalType;
            _audioSource.clip = _lootboxClicked;
            _audioSource.Play();
            
            StartCoroutine(OpenLootbox());
        }
        private IEnumerator OpenLootbox()
        {
            _lootboxActivated = true;
            _showBackAnimator.enabled = true;
            _showBackAnimator.SetTrigger(Open);
            yield return new WaitForSeconds(_timeAnimationShowBack);

            StartCoroutine(ViewRandomAnimation());

            yield return new WaitForSeconds(_countChangeRandom * _delayShowRandom);

            _droppedImage.sprite = _droppedAnimalType.Ico;
            _TakeButtonGO.SetActive(true);
            
        }
        private IEnumerator ViewRandomAnimation()
        {
            _showBackAnimator.enabled = false;
            _droppedImage.color = Color.white;
            
            var countChange = _countChangeRandom;
            var predId = int.MaxValue;
            _audioSource.clip = _randomRepeat;
            
            while (countChange > 0)
            {
                _audioSource.Play();

                possible = _selectedLootbox.GetPossibleAnimalItems();
                
                var curId = GetRandomWithoutRefNumber(0, possible.Count, predId);
                
                _droppedImage.sprite = _selectedLootbox.Animals[curId].AnimalType.Ico;

                predId = curId;
                countChange--;
                yield return new WaitForSeconds(_delayShowRandom);
            }
            
            yield return new WaitForSeconds(0.5f);

            _audioSource.clip = _randomReady;
            _audioSource.Play();
        }
        private int GetRandomWithoutRefNumber(int min, int max, int refNumber)
        {
            for (int i = 0; i < 10; i++)
            {
                var cur = Random.Range(min, max);

                if (cur != refNumber)
                    return cur;
            }
            
            return 0;
        }
        #endregion
        
        // V Code referenced by UnityEvents only V 
        public void Take()
        { 
            _audioSource.clip = _animalClicked;
            _audioSource.Play();
            _showBackAnimator.enabled = true;
            _showBackAnimator.SetTrigger(Close1);
            _lootboxActivated = false;
        }
        public void Close()
        {
            if (_lootboxActivated)
                return;
            
            _audioSource.clip = _close;
            _audioSource.Play();
            
            _panel.SetActive(false);
        }
        public void SwitchState()
        {  
            if (_lootboxActivated)
                return;
            
            if (_panel.activeSelf)
            {  
                _audioSource.clip = _close;
                _audioSource.Play();
                _panel.SetActive(false);
            }
            else
            { 
                _audioSource.clip = _open;
                _audioSource.Play();
                _panel.SetActive(true);
            }
        }
    }
}
