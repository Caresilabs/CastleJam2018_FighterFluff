using Assets.Scripts.Player.Fluff;
using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class UmbrellaSpikeAttack : Attack<UmbrellaPlayer>
    {
        public bool Attack;

        public float Damage;

        public float Speed = 5;

        public float WinddownTime = 1;

        public override void Update()
        {
            base.Update();

            if (Attack)
            {
                if (controller.Movement.Grounded)
                {
                    if (controller.OnWater)
                    {
                        FindObjectOfType<WaterController>().DecreaseWater(3);
                    }
                    Attack = false;
                    Invoke("SpikeOff", WinddownTime);
                    controller.Movement.LockMovement(WinddownTime);
                    return;
                }

                RaycastHit hit;

                controller.RigidBody.AddForce(Vector3.down * Speed, ForceMode.VelocityChange);

                Vector3 p1 = transform.position + GetComponent<CapsuleCollider>().center;

                if (Physics.SphereCast(p1, 2f, Vector3.down, out hit, 4f))
                {
                    PlayerController other = hit.transform.GetComponent<PlayerController>();
                    if (other != null)
                    {
                        Attack = false;
                        Invoke("SpikeOff", WinddownTime);
                        controller.Movement.LockMovement(WinddownTime);

                        // Small upforce
                        var selfVel = controller.RigidBody.velocity;
                        selfVel.y = 6;
                        controller.RigidBody.velocity = selfVel;

                        other.Damage(transform, Damage, 0.2f, 0.8f, other.Movement.Grounded ? 1.0f : -.6f); //0.25f

                        other.PlayerCamera.Shake(0.4f, 0.4f);
                        controller.PlayerCamera.Shake(0.3f, 0.3f);
                    }
                }
                else if (Physics.SphereCast(p1, 10, transform.forward, out hit, 4f))
                {
                    PlayerController other = hit.transform.GetComponent<PlayerController>();
                    if (other != null && other.Movement.Grounded)
                    {
                        other.Damage(transform, Damage * 0.333f, 0.02f, .3f, 20.5f);
                        other.Movement.LockMovement(WinddownTime*3.0f);
                        other.PlayerCamera.Shake(0.3f, 0.2f);
                    }
                }
            }
        }

        private void SpikeOff()
        {
            controller.Movement.Animator.SetBool("Spike", false);
        }

        public override bool CanUse()
        {
            if (controller.Movement.Grounded)
                return false;

            return base.CanUse();
        }

        public override void Use()
        {
            Attack = true;
            controller.Movement.Animator.SetBool("Spike", true);
            base.Use();
        }

        protected override InputLayout.ActionType[] GetKeys()
        {
            return new[] { InputLayout.ActionType.SPECIAL };
        }
    }
}
