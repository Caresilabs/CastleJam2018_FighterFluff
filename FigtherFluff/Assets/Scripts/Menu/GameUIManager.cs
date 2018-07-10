using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField]
        private Slider Player1Health;

        [SerializeField]
        private Slider Player2Health;

        private void Update()
        {
            Player1Health.value = GameManager.Instance.Player1.Health / GameManager.Instance.Player1.MaxHealth;
            Player2Health.value = GameManager.Instance.Player2.Health / GameManager.Instance.Player2.MaxHealth;
        }
    }
}
