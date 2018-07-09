﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class FluffPlayer : PlayerController
    {
        protected override void Start()
        {
            base.Start();

            MovementController movement = GetComponent<MovementController>();
            movement.onJump += () => {
                RigidBody.AddForce(new Vector3(0, 1000, 0));
            };
        }
    }
}
