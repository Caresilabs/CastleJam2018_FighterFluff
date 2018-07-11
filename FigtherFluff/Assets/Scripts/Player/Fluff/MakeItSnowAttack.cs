using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class MakeItSnowAttack : Attack<FluffPlayer>
    {
        [SerializeField]
        private WaterController Water;

        [SerializeField]
        private Transform SnowParticles;

        public bool Attack;

        public override void Update()
        {
            base.Update();

        }

        public override bool CanUse()
        {
            if (!controller.IsAxis(InputLayout.ActionType.MOVE_FORWARD, false))
            {
                return false;
            }

            return base.CanUse();
        }

        public override void Use()
        {
            Attack = false;
            Instantiate(SnowParticles, transform.position, Quaternion.LookRotation((GameManager.Instance.UmbrellaMan.transform.position - transform.position)), transform);
            base.Use();
        }

        protected override InputLayout.ActionType[] GetKeys()
        {
            return new[] { InputLayout.ActionType.SPECIAL };
        }
    }
}
