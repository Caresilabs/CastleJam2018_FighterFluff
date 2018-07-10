﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class MakeItBlowAttack : Attack<FluffPlayer>
    {
        public bool Attack;

        public float Damage = 5;

        public MakeItBlowAttack() : base(4f)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Attack)
            {
                if (Attack)
                {
                    RaycastHit hit;

                    Vector3 p1 = transform.position + GetComponent<CapsuleCollider>().center;

                    if (Physics.SphereCast(p1, 0.3f, transform.forward, out hit, 5))
                    {
                        controller.Movement.LockMovement(0.3f);

                        Debug.Log(hit.transform);
                        Attack = false;

                        PlayerController other = hit.transform.GetComponent<PlayerController>();
                        if (other != null)
                        {
                            controller.Movement.LockMovement(0.5f);
                            other.Damage(transform, Damage, 0.03f, 2, 0.3f);
                        }
                    }
                }
            }
        }

        public override bool CanUse()
        {
            return base.CanUse();
        }

        public override void Use()
        {
            Attack = false;
            base.Use();
        }

        protected override string[] GetKeys()
        {
            return new[] { "Attack" };
        }
    }
}