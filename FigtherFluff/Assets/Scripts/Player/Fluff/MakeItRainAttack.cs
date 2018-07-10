using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class MakeItRainAttack : Attack<FluffPlayer>
    {
        [SerializeField]
        private WaterController Water;

        public bool Attack;

        public MakeItRainAttack() : base(5f)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Attack)
            {
                
            }
        }

        public override bool CanUse()
        {
            return base.CanUse();
        }

        public override void Use()
        {
            Attack = false;
            Water.IncreaseWater();
            base.Use();
        }

        protected override string[] GetKeys()
        {
            return new[] { "Special" };
        }
    }
}
