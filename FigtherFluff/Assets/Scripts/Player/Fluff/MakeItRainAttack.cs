﻿using System;
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
            if (controller.IsAxis(InputLayout.ActionType.MOVE_FORWARD, true) || controller.IsAxis(InputLayout.ActionType.MOVE_FORWARD, false) 
                || controller.IsAxis(InputLayout.ActionType.MOVE_RIGHT, true) || controller.IsAxis(InputLayout.ActionType.MOVE_RIGHT, false))
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

        protected override InputLayout.ActionType[] GetKeys()
        {
            return new[] { InputLayout.ActionType.SPECIAL };
        }
    }
}
