using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class JumpAttack : Attack<UmbrellaPlayer>
    {

        [SerializeField]
        private float Damage = 5;

        private bool HasAttacked;

        public override void Update()
        {
            base.Update();

            if (!controller.Movement.Grounded)
            {
                if (HasAttacked)
                    return;

                var vel = controller.RigidBody.velocity;

                if (vel.y < 0)
                    return;

                RaycastHit hit;

                Vector3 p1 = transform.position + GetComponent<CapsuleCollider>().center;

                if (Physics.SphereCast(p1, 0.2f, Vector3.Lerp(transform.forward, Vector3.down, 0.1f), out hit, 4.6f))
                {
                    PlayerController other = hit.transform.GetComponent<PlayerController>();
                    if (other != null && !other.Movement.Grounded)
                    {
                        HasAttacked = true;

                        other.Damage(transform, Damage, 0.4f, 0, 0);

                        other.PlayerCamera.Shake(0.4f, 0.3f);
                        controller.PlayerCamera.Shake(0.4f, 0.3f);

                        vel.y = 0;
                        controller.RigidBody.velocity = vel;

                        other.RigidBody.velocity = Vector3.zero;
                        other.Movement.LockMovement(1);

                        controller.Movement.LockGravity(0.5f);
                    }
                }
            }
            else
            {
                HasAttacked = false;
            }
        }

        protected override InputLayout.ActionType[] GetKeys()
        {
            return null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            //if (!movementController.Grounded)
            //{
            //    PlayerController other = collision.transform.GetComponent<PlayerController>();
            //    if (other != null)
            //    {
            //        other.Damage(transform, Damage, 0.1f, 0, 0);
            //        var vel = controller.RigidBody.velocity;
            //        vel.y = 0;
            //        controller.RigidBody.velocity = vel;

            //        other.RigidBody.velocity = vel;

            //        controller.Movement.LockGravity(1.5f);
            //    }
            //}
        }
    }
}
