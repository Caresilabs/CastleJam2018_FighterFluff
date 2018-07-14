using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class MakeItBlowAttack : Attack<FluffPlayer>
    {
        [SerializeField]
        private Transform BlowParticles;

        [SerializeField]
        private AudioClip[] SoundEffects;

        public bool Attack;

        public float Damage = 5;

        public float WinddownTime;

        public override bool CanUse()
        {
            return base.CanUse();
        }

        public override void Use()
        {
            Attack = false;
            Transform attack =  Instantiate(BlowParticles, transform.position, transform.rotation, transform);
            attack.GetComponent<FluffParticleAttack>().Init(this, GameManager.Instance.UmbrellaMan.transform);

            controller.Movement.LockMovement(WinddownTime, true);

            controller.Movement.Animator.SetTrigger("Blow");

            MusicManager.Instance.PlaySound(SoundEffects[UnityEngine.Random.Range(0, SoundEffects.Length)], 0.9f);

            base.Use();
        }

        protected override InputLayout.ActionType[] GetKeys()
        {
            return new[] { InputLayout.ActionType.ATTACK };
        }
    }
}
