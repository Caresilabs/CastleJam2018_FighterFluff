using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class WaterController : MonoBehaviour
    {
        [SerializeField]
        private float FillHeight = 20;

        [SerializeField]
        private float IncreaseStep = 5;

        private float lowY;
        private float highY;
        private float currentY;

        private void Start()
        {
            this.lowY = transform.position.y;
            this.highY = lowY + FillHeight;
            this.currentY = lowY;
        }

        private void Update()
        {
            var target = transform.position;
            target.y = currentY;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 3);

            currentY = Mathf.Clamp(currentY - Time.deltaTime * 0.5f, lowY, highY);
        }

        public void IncreaseWater()
        {
            currentY = Mathf.Clamp(currentY + IncreaseStep, lowY, highY);
        }
    }
}
