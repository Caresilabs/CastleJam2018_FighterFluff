
using UnityEngine;

namespace Assets.Scripts.Player
{
    public abstract class Attack<T> : MonoBehaviour where T : PlayerController
    {
        private string[] keys;

        protected T controller;

        private readonly float cooldown;
        private float cooldownTimer;

        public Attack(float cooldown)
        {
            this.cooldown = cooldown;
        }

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
            this.cooldownTimer = cooldown;
        }

        public virtual bool CanUse()
        {
            return cooldownTimer < 0;
        }

        public bool IsKeysDown()
        {
            return controller.IsKeys(keys);
        }
    }
}
