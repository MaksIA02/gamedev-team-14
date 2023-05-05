using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Services.Updater;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Item.Behaviour
{
    public class SceneItem : MonoBehaviour
    {
        [SerializeField] private SpriteRender _sprite;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        [SerializeField] private Canvas canvas;

        [Header("DropAnimation")]
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _minVerticalPosition;
        [SerializeField] private float _maxVerticalPosition;
        [SerializeField] private Transform _itemTransform;
        

        [Header("DropAnimation")]
        [SerializeField] private float _dropAnimDuration;
        [SerializeField] private float _dropRotation;
        [SerializeField] private float _dropRadius;

        private float _sizeModificator;
        private Sequence _sequence;

        public event Action<SceneItem> ItemClicked;

        private bool _textEnabled = true;

        public bool TextEnabled
        {
            set
            {
                if (_textEnabled != true)
                    return;

                _textEnabled = value;
                _canvas.enabled = false;
            }
        }

        private void Awake()
        {
            _button.onClick.AddListener(()=>ItemClicked?.Invoke(this);
            float positionDifference = _maxVerticalPosition - _minVerticalPosition;
            float sizeDifference = _maxSize - _minSize;
            _sizeModificator = sizeDifference / positionDifference;
        }

        private void OnMouseDown() => ItemClicked?.Invoke(this);

        private void OnDestroy()
        {
            
        }

        public void SetItem(Sprite sprite, string itemName, Color textColor)
        {
            _sprite.sprite = sprite;
            _text.text = itemName;
            _text.color = textColor;
            _canvas.enebled = false;
        }

        public void PlayDrop(Vector2 position)
        {
            transform.position = position;
            Vector2 movePosition = transform.position + new Vector3(0, Random.Range(-_dropRadius, _dropRadius, 0);
            _sequence = DOTween.Sequence();
            _sequence.Join(transform.DOMove(movePosition, _dropAnimDuration));
            _sequence.Join(_itemTransform.DORotate(new Vector3(0, 0, Random.Range(-_dropRotation, _dropRotation)), _dropAnimDuration));
            _sequence.OnComplete(() => _canvas.enebled = _textEnabled);
        }

        private void UpdateSize()
        {
            float verticalDelta = _maxVerticalPosition - _itemTransform.position.y;
            float currentSizeModificator = _minSize + _sizeModificator * verticalDelta;
            _itemTransfrom.localScale = (Vector2.one * currentSizeModificator);
        }


    }
}

