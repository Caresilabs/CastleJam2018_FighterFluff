using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class ThunderBoltAttack : Attack<FluffPlayer>
    {
        [SerializeField]
        private WaterController Water;

        [SerializeField]
        private Transform LightParticles;

        [SerializeField]
        private AudioClip SoundEffect;

        public bool Attack;
        private bool hasAttacked;

        public float WinddownTime;

        public Color WaterHitColor = Color.yellow;

        public override void Update()
        {
            base.Update();

            if (Attack && !hasAttacked)
            {
                hasAttacked = true;

                MusicManager.Instance.PlaySound(SoundEffect, 0.95f);

                StartCoroutine(Light());

                Transform attack = Instantiate(LightParticles, transform.position, Quaternion.LookRotation((GameManager.Instance.UmbrellaMan.transform.position - transform.position)), transform);
                attack.GetComponent<FluffParticleAttack>().Init(this, GameManager.Instance.UmbrellaMan.transform);
            }
        }

        public override bool CanUse()
        {
            if (!controller.IsAxis(InputLayout.ActionType.MOVE_FORWARD, true))
                return false;

            return base.CanUse();
        }

        public override void Use()
        {
            //Attack = false;
            controller.Movement.Animator.SetTrigger("Thunder");
            controller.Movement.LockMovement(WinddownTime, true);
            hasAttacked = false;
            base.Use();
        }

        private IEnumerator Light()
        {
            var prevColor = GameManager.Instance.Player1.PlayerCamera.GetComponentInChildren<Camera>().backgroundColor;

            for (int i = 0; i < 2; i++)
            {
                GameManager.Instance.Player1.PlayerCamera.GetComponentInChildren<Camera>().backgroundColor = Color.black;
                GameManager.Instance.Player2.PlayerCamera.GetComponentInChildren<Camera>().backgroundColor = Color.black;

                yield return new WaitForSecondsRealtime(0.15f);

                GameManager.Instance.Player1.PlayerCamera.GetComponentInChildren<Camera>().backgroundColor = prevColor;
                GameManager.Instance.Player2.PlayerCamera.GetComponentInChildren<Camera>().backgroundColor = prevColor;

                yield return new WaitForSecondsRealtime(0.25f);
            }
           
        }

        public void LightHit()
        {
            //StartCoroutine(Light());
            StartCoroutine(LightHitCoroutine());
        }

        private IEnumerator LightHitCoroutine()
        {
            Material mat = Water.GetComponentInChildren<MeshRenderer>().sharedMaterial;
            var original = mat.GetColor("_BaseColor");
            mat.SetColor("_BaseColor", WaterHitColor);
            yield return new WaitForSecondsRealtime(0.2f);
            mat.SetColor("_BaseColor", original);
        }

        protected override InputLayout.ActionType[] GetKeys()
        {
            return new[] { InputLayout.ActionType.SPECIAL };
        }
    }
}
