using UnityEngine;

namespace Assets.Scripts.Player
{
    public abstract class Attack<T> : MonoBehaviour where T : PlayerController
    {
        private string[] keys;

        protected T controller;

        [SerializeField]
        protected float cooldown;

        [SerializeField]
        protected float Duration = 1;

        private float cooldownTimer;

        protected abstract string[] GetKeys();

        protected virtual void Start()
        {
            this.controller = GetComponent<T>();
            this.keys = GetKeys();
        }

        public virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;

            if (IsKeysDown())
            {
                if (CanUse())
                {
                    Use();
                }
            }
        }

        public virtual void Use()
        {
            controller.SetCooldown(Duration);
            this.cooldownTimer = cooldown;
        }

        public virtual bool CanUse()
        {
            return cooldownTimer < 0 && !controller.HasCooldown();
        }

        public bool IsKeysDown()
        {
            return controller.IsKeys(keys);
        }
    }
}
