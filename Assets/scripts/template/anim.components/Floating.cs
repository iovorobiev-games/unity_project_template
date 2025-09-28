using System;
using DG.Tweening;
using UnityEngine;

namespace template.anim.components
{
    public class Floating : MonoBehaviour
    {
        public bool launchOnStart;
        public float amplitude = 1f;
        public float speedSec = 1f;
        public float shakeScale = 0.5f;

        private Vector3 _initPos;

        private void Start()
        {
            _initPos = transform.position;
            if (launchOnStart)
            {
                launch();
            }
        }

        public void launch()
        {
            transform.DOKill();
            transform.position = _initPos;
            DOTween.Sequence()
                .Append(transform.DOMoveY(transform.position.y - amplitude / 2f, speedSec / 2f)
                    .SetLoops(2, LoopType.Yoyo))
                .Append(transform.DOMoveY(transform.position.y + amplitude / 2f, speedSec / 2f)
                    .SetLoops(2, LoopType.Yoyo)).SetLoops(-1, LoopType.Restart);
            if (shakeScale > 0f)
            {
                DOTween.Sequence()
                    .Append(transform
                        .DORotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - shakeScale),
                            speedSec / 4f).SetLoops(2, LoopType.Yoyo))
                    .Append(transform
                        .DORotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + shakeScale),
                            speedSec / 4f).SetLoops(2, LoopType.Yoyo))
                    .SetLoops(-1, LoopType.Restart);
            }
        }
    }
}