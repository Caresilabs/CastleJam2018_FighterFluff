using UnityEngine;

namespace Assets.Scripts.Player.UmbrellaMan
{
    public class UmbrellaAttack : Attack<UmbrellaPlayer>
    {

        public bool Attack;

        public float Damage;

        protected override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();

            if (Attack)
            {
                RaycastHit hit;

                Vector3 p1 = transform.position/* + GetComponent<CapsuleCollider>().center*/;

                if (Physics.SphereCast(p1, 0.5f, transform.forward, out hit, 4))
                {
                    Attack = false;

                    PlayerController other = hit.transform.GetComponent<PlayerController>();
                    if (other != null)
                    {
                        other.Damage(transform, Damage, 0.03f, .8f, controller.Movement.Grounded ? 0.8f : -1.6f);
                    }
                }
            }
        }

        public override bool CanUse()
        {
            return base.CanUse();
        }

        public override void Use()
        {
            Attack = true;
            base.Use();
        }

        protected override InputLayout.ActionType[] GetKeys()
        {
            return new[] { InputLayout.ActionType.ATTACK };
        }
    }
}
