using UnityEngine;
using UnityEngine.UI;

namespace CollectCreatures
{


    public class SpriteBar : MonoBehaviour
    {
        #region VAR
        [SerializeField] private float _speedChange = 10;
        [Space(20)] [SerializeField] private Image _image;
        [SerializeField] private bool _changeColor;
        [SerializeField] private Color _beginColor;
        [SerializeField] private Color _endColor;

        [SerializeField] private IntWithEvent curCount;
        [SerializeField] private IntWithEvent maxCount;

        private float _differenceR;
        private float _differenceG;
        private float _differenceB;
        private float _beginLocalScaleX;
        #endregion

        #region MONO
        private void Start()
        {
            if (_changeColor)
                Init();

            _beginLocalScaleX = transform.localScale.x;
        }
        private void Update()
        {
            ChangeBar();

            if (_changeColor)
                ChangeColor();
        }
        #endregion
        
        #region FUNC
        private void Init()
        {
            _image = GetComponent<Image>();
            _image.color = _beginColor;
            _differenceR = _beginColor.r - _endColor.r;
            _differenceG = _beginColor.g - _endColor.g;
            _differenceB = _beginColor.b - _endColor.b;
        }
        private void ChangeBar()
        {
            var tempVector = transform.localScale;
            tempVector.x = Mathf.Lerp(transform.localScale.x, _beginLocalScaleX * curCount.Count / maxCount.Count,
                Time.deltaTime * _speedChange);
            transform.localScale = tempVector;
        }
        private void ChangeColor()
        {
            Color color = new Color();
            color.r = _endColor.r + transform.localScale.x * _differenceR;
            color.b = _endColor.b + transform.localScale.x * _differenceB;
            color.g = _endColor.g + transform.localScale.x * _differenceG;
            color.a = 100;
            _image.color = color;
        }
        #endregion
    }
}