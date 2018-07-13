using System;
using UnityEngine;

namespace Assets.Scripts.Player.Fluff
{
    public class WaterController : MonoBehaviour
    {
        [SerializeField]
        private float FillHeight = 20;

        [SerializeField]
        private float IncreaseStep = 5;

        [SerializeField]
        private float DecreasePerSecond = 0.4f;

        private Rigidbody Rigidbody;

        private float lowY;
        private float highY;
        private float currentY;

        private void Start()
        {
            this.lowY = transform.position.y;
            this.highY = lowY + FillHeight;
            this.currentY = lowY;
        }

        internal void DecreaseWater(int v)
        {
            currentY = Mathf.Clamp(currentY - v, lowY, highY);
        }

        private void Update()
        {
            Rigidbody = GetComponent<Rigidbody>();

            var target = transform.position;
            target.y = currentY;

            Rigidbody.MovePosition(Vector3.Lerp(transform.position, target, Time.deltaTime * 3));
            //transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 3);

            currentY = Mathf.Clamp(currentY - Time.deltaTime * DecreasePerSecond, lowY, highY);
        }

        public void IncreaseWater()
        {
            currentY = Mathf.Clamp(currentY + IncreaseStep, lowY, highY);
        }
    }
}
