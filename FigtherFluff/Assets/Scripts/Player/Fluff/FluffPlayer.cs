using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class FluffPlayer : PlayerController
    {
        [SerializeField]
        private float AirTime = 3;

        private bool didJump = false;

        protected override void Start()
        {
            base.Start();

            MovementController movement = GetComponent<MovementController>();
            movement.onJump += OnJump;
        }
       
        private void OnJump()
        {
            RigidBody.AddForce(new Vector3(0, 800, 0));
            didJump = true;
        }

        protected override void Update()
        {
            base.Update();

            if (didJump)
            {
                if (RigidBody.velocity.y < 0)
                {
                    var velocity = RigidBody.velocity;
                    velocity.y = 0;
                    RigidBody.velocity = velocity;
                    Movement.LockGravity(AirTime);
                    didJump = false;
                }
            }
        }

    }
}
