using System;
using System.Numerics;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

namespace utils
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AwaitableClickHandlerImpl: MonoBehaviour, IAwaitablePointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private AwaitableClickListener<GameObject> _listener = new();
        public Color defaultTint = Color.white;
        public Color hoverTint = Color.clear;
        public Vector3 deltaMoveOnClick = Vector3.zero;
        public SpriteRenderer buttonSprite;

        private Vector3 originalPos;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _listener.notifyClick(gameObject);
        }

        public async UniTask onClickAwaitable()
        {
            await _listener.awaitClick();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (hoverTint != Color.clear && buttonSprite != null)
            {
                buttonSprite.color = hoverTint;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buttonSprite != null)
            {
                buttonSprite.color = defaultTint;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            originalPos = transform.position;
            if (deltaMoveOnClick != Vector3.zero)
            {
                transform.position += deltaMoveOnClick;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.position = originalPos;
        }
    }
}