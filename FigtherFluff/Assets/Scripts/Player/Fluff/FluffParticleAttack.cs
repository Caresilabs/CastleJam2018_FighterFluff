using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class FluffParticleAttack : MonoBehaviour
    {
        public Attack<FluffPlayer> AttackSource { get; set; }

        public float Damage = 5;
        public float KnockbackHeight;
        public float Knockback;
        public float Hitstun;

        public float ShakeDuration;
        public float ShakeStrength;

        public float RetargetAlpha = 30f;

        private PlayerController UmbrellaMan;

        private void Awake()
        {
            this.UmbrellaMan = GameManager.Instance.UmbrellaMan;    //GameObject.FindObjectOfType<UmbrellaMan.UmbrellaPlayer>();
            lastDir = (UmbrellaMan.transform.position - transform.position).normalized;
        }

        private Vector3 lastDir;
        private void LateUpdate()
        {
            Vector3 fwd = UmbrellaMan.transform.position - transform.position;
            fwd.Normalize();

            var newLook = Vector3.Slerp(lastDir, fwd, Time.deltaTime * RetargetAlpha);

            transform.rotation = Quaternion.LookRotation(newLook); // Quaternion.Slerp(Quaternion.LookRotation(lastDir), Quaternion.LookRotation(fwd), Time.deltaTime * 0.001f);

            lastDir = newLook;
        }

        public void OnParticleHit(PlayerController other, float blockFactor, bool onWater)
        {
            if (onWater)
            {
                if (AttackSource is ThunderBoltAttack)
                {
                    var light = AttackSource as ThunderBoltAttack;
                    light.LightHit();

                    blockFactor = 2; // hack

                   // Damage *= 2;
                    Knockback = 2;
                    KnockbackHeight = 99999; // hack
                    Hitstun *= 1.5f;

                    other.Movement.LockMovement(1.5f);
                }
            }

            if (AttackSource is MakeItSnowAttack)
            {
                var snow = AttackSource as MakeItSnowAttack;
                other.Movement.Slow(snow.SlowTime, snow.SlowScale);
            }

            other.Damage(GameManager.Instance.Fluff.transform, Damage * blockFactor, Hitstun, Knockback * blockFactor, KnockbackHeight * blockFactor);
            other.PlayerCamera.Shake(ShakeDuration, ShakeStrength * blockFactor);

            GameManager.Instance.Fluff.PlayerCamera.Shake(0.2f * blockFactor, 0.3f * blockFactor);

            Destroy(this);
        }

    }
}
