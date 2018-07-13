using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class MakeItSnowAttack : Attack<FluffPlayer>
    {
        [SerializeField]
        private WaterController Water;

        [SerializeField]
        private Transform SnowParticles;

        [SerializeField]
        private AudioClip[] SoundEffects;

        public float SlowTime;
        public float SlowScale;

        public float WinddownTime;

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
            Transform attack = Instantiate(SnowParticles, transform.position, Quaternion.LookRotation((GameManager.Instance.UmbrellaMan.transform.position - transform.position)), transform);
            attack.GetComponent<FluffParticleAttack>().Init(this, GameManager.Instance.UmbrellaMan.transform);
            controller.Movement.LockMovement(WinddownTime, true);

            controller.Movement.Animator.SetTrigger("Water");

            MusicManager.Instance.PlaySound(SoundEffects[UnityEngine.Random.Range(0, SoundEffects.Length - 1)], 0.9f);

            base.Use();
        }

        protected override InputLayout.ActionType[] GetKeys()
        {
            return new[] { InputLayout.ActionType.SPECIAL };
        }
    }
}
