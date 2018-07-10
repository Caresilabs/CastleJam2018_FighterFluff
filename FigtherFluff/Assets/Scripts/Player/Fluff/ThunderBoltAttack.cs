using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class ThunderBoltAttack : Attack<FluffPlayer>
    {
        [SerializeField]
        private WaterController Water;

        public bool Attack;

        public ThunderBoltAttack() : base(5f)
        {
        }

        public override void Update()
        {
            base.Update();

            if (Attack)
            {
                
            }
        }

        public override bool CanUse()
        {
            return base.CanUse();
        }

        public override void Use()
        {
            Attack = false;
            base.Use();
        }

        protected override string[] GetKeys()
        {
            return new[] { "Special" };
        }
    }
}
