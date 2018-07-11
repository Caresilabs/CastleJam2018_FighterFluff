using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    controller.Movement.LockMovement(PenaltyTime);
                }
            }
        }

        public override bool CanUse()
        {
            if (!controller.Movement.Grounded)
                return false;

            return base.CanUse();
        }

        public override void Use()
        {
            blockTime = 0;
            isBlocking = true;
            controller.Movement.LockMovement();
            base.Use();
        }

        public float TryBlock(Transform attack)
        {
            if (!isBlocking)
                return 1;

            if (blockTime < ReflectTime)
            {
                Destroy(attack.gameObject);
                controller.Movement.UnlockMovement();
            }
            else if (blockTime < ReflectTime + BlockTime)
            {
                Destroy(attack.gameObject);
                controller.Movement.UnlockMovement();
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
