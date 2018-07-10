using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class DashAttack : Attack<UmbrellaPlayer>
    {
        [SerializeField]
        private bool DashRight;

        [SerializeField]
        private float DashSpeed = 35;

        public DashAttack() : base(1.5f)
        {
            //GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).First().clip.
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void Use()
        {
            base.Use();

            var force = Vector3.Lerp(DashRight ? transform.right : -transform.right, transform.forward, 0.5f) * DashSpeed;
            var velocity = controller.RigidBody.velocity;
            velocity.x = force.x;
            velocity.z = force.z;

            controller.RigidBody.velocity = velocity;

            //controller.RigidBody.AddForce(, ForceMode.VelocityChange);
            GetComponent<MovementController>().LockMovement(0.5f); // TODO cache
        }

        protected override string[] GetKeys()
        {
            return DashRight ? new[] { "ShoulderRight" } : new[] { "ShoulderLeft" };
        }
    }
}
