using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class UmbrellaBlock : Attack<UmbrellaPlayer>
    {

        public UmbrellaBlock() : base(2)
        {
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();

            //if (Attack)
            //{
            //    RaycastHit hit;

            //    Vector3 p1 = transform.position + GetComponent<CapsuleCollider>().center;
            //    float distanceToObstacle = 0;

            //    // Cast a sphere wrapping character controller 10 meters forward
            //    // to see if it is about to hit anything.
            //    if (Physics.SphereCast(p1, 0.5f, transform.forward, out hit, 2))
            //    {
            //        Debug.Log(hit.transform);
            //        distanceToObstacle = hit.distance;
            //        Attack = false;

            //        PlayerController other = hit.transform.GetComponent<PlayerController>();
            //        if (other != null)
            //        {
            //            other.Damage(transform, Damage, 0.03f, .6f, 0.8f);
            //        }
            //    }
            //}
        }

        public override bool CanUse()
        {
            return base.CanUse();
        }

        public override void Use()
        {
            base.Use();
        }

        protected override string[] GetKeys()
        {
            return new[] { "Special" };
        }
    }
}
