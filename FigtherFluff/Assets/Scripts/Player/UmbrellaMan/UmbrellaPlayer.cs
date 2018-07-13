﻿using Assets.Scripts.Player.Fluff;
using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class UmbrellaPlayer : PlayerController
    {
        [SerializeField]
        private float WaterSpeedScale = 0.5f;

        [SerializeField]
        private AudioClip[] HitSounds;

        public bool OnWater { get; private set; }

        protected override void Start()
        {
            base.Start();
            Movement.onJump += OnJump;
        }

        private void OnJump()
        {
            RigidBody.AddForce(new Vector3(0, 800 * 2, 0));
        }

        public override void Damage(Transform source, float damage, float stunTime, float knockback, float knockbackHeight = 0)
        {
            base.Damage(source, damage, stunTime, knockback, knockbackHeight);
            MusicManager.Instance.PlaySound(HitSounds[Random.Range(0, HitSounds.Length - 1)], 0.8f);
        }

        protected override void Update()
        {
            base.Update();
            Movement.Animator.SetFloat("VerticalSpeed", RigidBody.velocity.y);
        }

        private void OnParticleCollision(GameObject other)
        {
            FluffParticleAttack attack = other.GetComponentInParent<FluffParticleAttack>();
            if (attack != null && attack.AttackSource != null)
            {
                var blockFactor = GetComponent<UmbrellaBlock>().TryBlock(attack.AttackSource.transform, attack.transform);
                if (blockFactor != 0)
                {
                    attack.OnParticleHit(this, blockFactor, OnWater);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Water"))
            {
                Movement.SpeedScale = (WaterSpeedScale);
                OnWater = true;
                Movement.Animator.SetBool("IsFloating", true);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Water"))
            {
                Movement.SpeedScale = 1;
                OnWater = false;
                Movement.Animator.SetBool("IsFloating", false);
            }
        }

    }
}
