using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class UmbrellaAttack : Attack<UmbrellaPlayer>
    {

        public bool Attack;

        public float Damage;

        public float ForwardSpeed = 10;

        private bool hasAttacked;

        protected override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();

            if (Attack && !hasAttacked)
            {
                RaycastHit hit;

                Vector3 p1 = transform.position /* + GetComponent<CapsuleCollider>().center*/;

                if (Physics.SphereCast(p1, 0.5f, transform.forward, out hit, 4f))
                {
                    PlayerController other = hit.transform.GetComponent<PlayerController>();
                    if (other != null)
                    {
                        Attack = false;
                        hasAttacked = true;

                        controller.PlayerCamera.Shake(0.2f, 0.1f);

                        other.Damage(transform, Damage, 0.03f, .5f, controller.Movement.Grounded ? 3.5f : -3.8f);
                        if (controller.Movement.Grounded)
                        {
                            //controller.Movement.LockGravity(0.5f);
                            //controller.Movement.LockMovement(0.01f, true);
                            other.Movement.LockMovement(0.3f);
                        }
                        else
                        {
                            other.Movement.LockMovement(1.8f);
                        }
                    }
                }
            }
        }

        public override bool CanUse()
        {
            if (controller.OnWater)
                return false;

            return base.CanUse();
        }

        public override void Use()
        {
            controller.RigidBody.AddForce(transform.forward * ForwardSpeed, ForceMode.VelocityChange);
            Attack = true;
            hasAttacked = false;
            controller.Movement.Animator.SetTrigger("Attacked");
            base.Use();
        }

        protected override InputLayout.ActionType[] GetKeys()
        {
            return new[] { InputLayout.ActionType.ATTACK };
        }
    }
}
