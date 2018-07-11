using Assets.Scripts.Player.Fluff;
using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class UmbrellaPlayer : PlayerController
    {
        [SerializeField]
        private float WaterSpeedScale = 0.5f;

        public bool OnWater { get; private set; }

        protected override void Start()
        {
            base.Start();
            Movement.onJump += OnJump;
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
                var blockFactor = GetComponent<UmbrellaBlock>().TryBlock(attack.transform);
                if (blockFactor != 0)
                {
                    attack.OnParticleHit(this, blockFactor);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Water"))
            {
                Movement.SpeedScale = (WaterSpeedScale);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Water"))
            {
                Movement.SpeedScale = 1;
            }
        }

    }
}
