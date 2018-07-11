﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class ThunderBoltAttack : Attack<FluffPlayer>
    {
        [SerializeField]
        private WaterController Water;

        [SerializeField]
        private Transform LightParticles;

        public bool Attack;

        //public override void Update()
        //{
        //    base.Update();

        //    if (Attack)
        //    {
                
        //    }
        //}

        public override bool CanUse()
        {
            if (!controller.IsAxis("Vertical", true))
                return false;

            return base.CanUse();
        }

        public override void Use()
        {
            StartCoroutine(Light());

            Instantiate(LightParticles, transform.position, Quaternion.LookRotation((GameManager.Instance.UmbrellaMan.transform.position - transform.position)), transform);
            Attack = false;
            base.Use();
        }

        private IEnumerator Light()
        {
            var prevColor = GameManager.Instance.Player1.PlayerCamera.GetComponentInChildren<Camera>().backgroundColor;

            for (int i = 0; i < 2; i++)
            {
                GameManager.Instance.Player1.PlayerCamera.GetComponentInChildren<Camera>().backgroundColor = Color.black;
                GameManager.Instance.Player2.PlayerCamera.GetComponentInChildren<Camera>().backgroundColor = Color.black;

                yield return new WaitForSecondsRealtime(0.15f);

                GameManager.Instance.Player1.PlayerCamera.GetComponentInChildren<Camera>().backgroundColor = prevColor;
                GameManager.Instance.Player2.PlayerCamera.GetComponentInChildren<Camera>().backgroundColor = prevColor;

                yield return new WaitForSecondsRealtime(0.25f);
            }
           
        }

        protected override string[] GetKeys()
        {
            return new[] { "Special" };
        }
    }
}
