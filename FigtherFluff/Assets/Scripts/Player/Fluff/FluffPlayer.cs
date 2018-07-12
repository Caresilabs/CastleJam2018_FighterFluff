using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class FluffPlayer : PlayerController
    {
        [SerializeField]
        private Transform FluffHitParticles;

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

        private void OnParticleCollision(GameObject other)
        {
            FluffParticleAttack attack = other.GetComponentInParent<FluffParticleAttack>();
            if (attack != null && attack.AttackSource == null)
            {
                attack.OnParticleHit(this, 1, false);
            }
        }

        public override void Damage(Transform source, float damage, float stunTime, float knockback, float knockbackHeight = 0)
        {
            base.Damage(source, damage, stunTime, knockback, knockbackHeight);
            Instantiate(FluffHitParticles, transform);
        }

    }
}
