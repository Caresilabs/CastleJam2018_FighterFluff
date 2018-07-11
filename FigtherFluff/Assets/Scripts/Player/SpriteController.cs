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
        public class FAnimation
        {
            public string name;
            public Sprite[] sprite;
        }
        [SerializeField]
        public FAnimation[] Animations;

        //private Dictionary<int, Sprite[]> AnimationsMap;
        private Dictionary<int, FAnimation[]> AnimationsMap;

        [SerializeField]
        private string CurrentSpriteName = "idle";

        private int CurrentSpriteHash;

        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            animator = transform.parent.GetComponent<Animator>();

            //AnimationsMap = Animations.ToDictionary(x => Animator.StringToHash(x.name), x => x.sprite);
            AnimationsMap = new Dictionary<int, FAnimation[]>();
            foreach (var item in Animations)
            {
                string name = item.name.Substring(0, item.name.Length - 2);
                int hash = Animator.StringToHash(name);
                if (AnimationsMap.ContainsKey(hash)) {
                    AnimationsMap[hash][1] = item;
                }
                else
                {
                    AnimationsMap.Add(hash, new FAnimation[2] { item, null });
                }
            }

            CurrentSpriteHash = Animator.StringToHash(CurrentSpriteName);

            spriteRenderer = GetComponent<SpriteRenderer>();

            CameraHolder.onPreCull += BeforeCameraRender;

            animator.GetBehaviour<OnStateChangeBehaviour>().CallbackFunc += (AnimatorStateInfo x) => {
                CurrentSpriteHash = x.shortNameHash;
            };
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
                //  spriteRenderer.sprite = AnimationsMap[CurrentSpriteName + "_f"][CurrentSpriteIndex];
                spriteRenderer.sprite = AnimationsMap[CurrentSpriteHash][0].sprite[CurrentSpriteIndex];
            }
            else
            {
                // Back
                spriteRenderer.sprite = AnimationsMap[CurrentSpriteHash][1].sprite[CurrentSpriteIndex];
                // spriteRenderer.sprite = AnimationsMap[CurrentSpriteName + "_b"][CurrentSpriteIndex];
            }
        }

        private void OnDestroy()
        {
            CameraHolder.onPreCull -= BeforeCameraRender;
        }

        //public void ChangeAnimation(string name)
        //{
        //   // animator.Play(name);
        //    CurrentSpriteName = name;
        //}

    }
}
