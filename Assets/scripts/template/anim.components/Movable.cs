using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace template.anim.components
{
    [RequireComponent(typeof(BoxCollider2D)), RequireComponent(typeof(Rigidbody2D))]
    public class Movable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private bool isDragging = false;
        private Camera mainCamera;
        private Vector3 offset;

        private UniTaskCompletionSource<Collider2D[]> dragEndSource;
        private UniTaskCompletionSource dragStartSource;
        
        private HashSet<Collider2D> dragEndSet = new();
        
        private void Start()
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                mainCamera = FindObjectOfType<Camera>();
            }
        }

        public UniTask dragStart()
        {
            dragStartSource?.TrySetCanceled();
            dragStartSource = new();
            return dragStartSource.Task;
        }

        public async UniTask<Collider2D[]> dragEnd()
        {
            dragEndSource?.TrySetCanceled();
            dragEndSource = new();
            return await dragEndSource.Task;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isDragging = true;

            // Calculate offset between object position and mouse position
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(eventData.position);
            worldPosition.z = transform.position.z; // Keep original Z position
            offset = transform.position - worldPosition;
            
            dragStartSource?.TrySetResult();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isDragging)
            {
                dragEndSource?.TrySetResult(dragEndSet.ToArray());
            }
            isDragging = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isDragging)
            {
                dragEndSet.Add(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!isDragging) return;
            dragEndSet.Remove(other);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;
            var worldPosition = mainCamera.ScreenToWorldPoint(eventData.position);
            worldPosition.z = transform.position.z; // Keep original Z position
            transform.position = worldPosition + offset;
        }
    }
}