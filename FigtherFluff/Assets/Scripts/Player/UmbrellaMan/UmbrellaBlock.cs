﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Player.Fluff;
using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class UmbrellaBlock : Attack<UmbrellaPlayer>
    {
        [SerializeField]
        private float ReflectTime;

        [SerializeField]
        private float BlockTime;

        [SerializeField]
        private float BlockDamageTime;

        [SerializeField]
        private float PenaltyTime;

        [SerializeField]
        private Transform BlockTooltip;

        [SerializeField]
        private Transform ReflectTooltip;


        [SerializeField]
        private AudioClip ReflectSound;

        [SerializeField]
        private AudioClip BlockSound;

        private float blockTime;
        private bool isBlocking;

        private float TotalTime { get { return ReflectTime + BlockTime + BlockDamageTime; } }

        public override void Update()
        {
            base.Update();
            if (isBlocking)
            {
                blockTime += Time.deltaTime;
                if (blockTime > TotalTime)
                {
                    isBlocking = false;
                    controller.Movement.Animator.SetBool("Shield", false);
                    controller.Movement.LockMovement(PenaltyTime);
                }
            }
        }

        public override bool CanUse()
        {
            if (!controller.Movement.Grounded)
                return false;

            if (controller.OnWater)
                return false;

            return base.CanUse();
        }

        public override void Use()
        {
            blockTime = 0;
            isBlocking = true;
            controller.Movement.Animator.SetBool("Shield", true);
            controller.Movement.LockMovement(true);
            controller.RigidBody.velocity *= 0.3f;
            base.Use();
        }

        public float TryBlock(Transform source, Transform attack)
        {
            if (!isBlocking)
                return 1;

            if (!attack.gameObject.activeInHierarchy)
                return 1;

            if (blockTime < ReflectTime)
            {
                Transform reflect = Instantiate(attack, transform.position, Quaternion.LookRotation(attack.position - transform.position), transform);
                reflect.GetComponent<FluffParticleAttack>().Init(null, source);

                Instantiate(ReflectTooltip, transform.position + new Vector3(0, 3,0), transform.rotation);
                attack.gameObject.SetActive(false);
                Destroy(attack.gameObject);
                controller.Movement.UnlockMovement();
                isBlocking = false;
                controller.Movement.Animator.SetBool("Shield", false);
                MusicManager.Instance.PlaySound(ReflectSound, 0.9f);
            }
            else if (blockTime < ReflectTime + BlockTime)
            {
                Destroy(attack.gameObject);
                Instantiate(BlockTooltip, transform.position + new Vector3(0, 3, 0), transform.rotation);
                attack.gameObject.SetActive(false);
                controller.Movement.UnlockMovement();
                isBlocking = false;
                controller.Movement.Animator.SetBool("Shield", false);
                MusicManager.Instance.PlaySound(BlockSound, 0.9f);
            }
            else if (blockTime < ReflectTime + BlockTime + BlockDamageTime)
            {
                //controller.Damage()
                return 0.5f;
            }

            return 0;
        }

        protected override InputLayout.ActionType[] GetKeys()
        {
            return new[] { InputLayout.ActionType.SPECIAL };
        }
    }
}
