using Assets.Scripts.Player.Fluff;
using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class UmbrellaPlayer : PlayerController
    {
        protected override void Start()
        {
            base.Start();

            MovementController movement = GetComponent<MovementController>();
            movement.onJump += OnJump;
        }

        private void OnJump()
        {
            RigidBody.AddForce(new Vector3(0, 900, 0));
        }

        private void OnParticleCollision(GameObject other)
        {
            FluffParticleAttack attack = other.GetComponentInParent<FluffParticleAttack>();
            if (attack != null)
            {
                attack.OnParticleHit(this);
            }
        }

    }
}
