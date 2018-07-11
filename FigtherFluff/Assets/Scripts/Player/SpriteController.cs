using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class SpriteController : MonoBehaviour
    {
        [SerializeField]
        private int CurrentSpriteIndex;

        [Serializable]
        public struct FAnimation
        {
            public string name;
            public Sprite[] sprite;
        }
        [SerializeField]
        public FAnimation[] Animations;

        private Dictionary<string, Sprite[]> AnimationsMap;

        [SerializeField]
        private string CurrentSpriteName = "idle";

        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            AnimationsMap = Animations.ToDictionary(x => x.name, x => x.sprite);

            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = transform.parent.GetComponent<Animator>();

            CameraHolder.onPreCull += BeforeCameraRender;
        }

        private void BeforeCameraRender(Transform cam)
        {
            if (!enabled)
                return;

            Vector3 fwd = cam.transform.forward;
            fwd.y = 0;
            transform.rotation = Quaternion.LookRotation(fwd);

            if (Vector3.Dot(transform.parent.forward, fwd) <= 0)
            {
                // Forward
                spriteRenderer.sprite = AnimationsMap[CurrentSpriteName + "_f"][CurrentSpriteIndex];
            }
            else
            {
                // Back
                spriteRenderer.sprite = AnimationsMap[CurrentSpriteName + "_b"][CurrentSpriteIndex];
            }
        }

        private void OnDestroy()
        {
            CameraHolder.onPreCull -= BeforeCameraRender;
        }

        public void ChangeAnimation(string name)
        {
            animator.Play(name);
            CurrentSpriteName = name;
        }
    }
}
