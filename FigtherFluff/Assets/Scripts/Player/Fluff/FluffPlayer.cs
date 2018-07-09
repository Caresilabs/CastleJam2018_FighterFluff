using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class FluffPlayer : MonoBehaviour
    {
        private Rigidbody RigidBody;

        private void Start()
        {
            this.RigidBody = GetComponent<Rigidbody>();

            MovementController movement = GetComponent<MovementController>();
            movement.onJump += () => {
                RigidBody.AddForce(new Vector3(0, 1000, 0));
            };
        }
    }
}
