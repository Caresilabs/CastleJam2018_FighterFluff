using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class DashAttack : Attack<UmbrellaPlayer>
    {
        [SerializeField]
        private bool DashRight;

        [SerializeField]
        private float DashSpeed = 35;

        [SerializeField]
        private float DashTime = 0.25f;

        [SerializeField]
        private AudioClip SoundEffect;

        protected override void Start()
        {
            base.Start();
        }

        public override void Use()
        {
            base.Use();
            controller.Movement.Animator.SetTrigger("Dashed");
            MusicManager.Instance.PlaySound(SoundEffect, 1f);

            StartCoroutine(DoDash());
        }

        private IEnumerator DoDash()
        {
            var startVelocity = controller.RigidBody.velocity;
            var force = Vector3.Lerp(DashRight ? transform.right : -transform.right, transform.forward, 0.5f) * DashSpeed;
            var velocity = startVelocity;
            velocity.x = force.x;
            velocity.z = force.z;

            controller.RigidBody.velocity = velocity;

           controller.Movement.LockMovement(DashTime); 
            yield return new WaitForSeconds(DashTime);

            startVelocity.y = controller.RigidBody.velocity.y;
            controller.RigidBody.velocity = startVelocity;

        }

        public override bool CanUse()
        {
            if (controller.OnWater)
                return false;

            return base.CanUse();
        }

        protected override InputLayout.ActionType[] GetKeys()
        {
            return DashRight ? new[] { InputLayout.ActionType.SHOULDER_RIGHT } : new[] { InputLayout.ActionType.SHOULDER_LEFT };
        }
    }
}
