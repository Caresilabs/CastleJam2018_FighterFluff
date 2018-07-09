using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class JumpAttack : Attack<UmbrellaPlayer>
    {

        [SerializeField]
        private float Damage = 5;

        private MovementController movementController;

        public JumpAttack() : base(0)
        {
        }

        protected override void Start()
        {
            base.Start();
            this.movementController = GetComponent<MovementController>();
        }

        protected override string[] GetKeys()
        {
            return null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!movementController.Grounded)
            {
                PlayerController other = collision.transform.GetComponent<PlayerController>();
                if (other != null)
                {
                    other.Damage(transform, Damage, 0.1f, 0, 0);
                }
            }
        }
    }
}
