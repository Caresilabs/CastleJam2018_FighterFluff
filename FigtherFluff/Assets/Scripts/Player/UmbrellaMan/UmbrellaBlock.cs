using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class UmbrellaBlock : Attack<UmbrellaPlayer>
    {
        private float blockTime;

        protected override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();

            blockTime += Time.deltaTime;

        }

        public override bool CanUse()
        {
            if (controller.Movement.Grounded)
                return false;

            return base.CanUse();
        }

        public override void Use()
        {
            blockTime = 0;
            base.Use();
        }

        protected override string[] GetKeys()
        {
            return new[] { "Special" };
        }
    }
}
