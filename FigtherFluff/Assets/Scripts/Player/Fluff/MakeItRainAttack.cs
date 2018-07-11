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

        [SerializeField]
        private Transform RainPartiles;

        public bool Attack;

        public override void Update()
        {
            base.Update();

            if (Attack)
            {

            }
        }

        public override bool CanUse()
        {
            if (controller.IsAxis("Vertical", true) || controller.IsAxis("Vertical", false) || controller.IsAxis("Horizontal", true) || controller.IsAxis("Horizontal", false))
                return false;

            return base.CanUse();
        }

        public override void Use()
        {
            Attack = false;
            Instantiate(RainPartiles, transform.position, Quaternion.LookRotation((GameManager.Instance.UmbrellaMan.transform.position - transform.position)), transform);
            Water.IncreaseWater();
            base.Use();
        }

        protected override string[] GetKeys()
        {
            return new[] { "Special" };
        }
    }
}
