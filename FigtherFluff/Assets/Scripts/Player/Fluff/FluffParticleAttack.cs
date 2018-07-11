using Assets.Scripts.Player.UmbrellaMan;
using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class FluffParticleAttack : MonoBehaviour
    {
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

        public void OnParticleHit(PlayerController other)
        {
            other.Damage(GameManager.Instance.Fluff.transform, Damage, Hitstun, Knockback, KnockbackHeight);
            other.PlayerCamera.Shake(ShakeDuration, ShakeStrength);

            GameManager.Instance.Fluff.PlayerCamera.Shake(0.15f, 0.3f);

            Destroy(this);
        }

    }
}
