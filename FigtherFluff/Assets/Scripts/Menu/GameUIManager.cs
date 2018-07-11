using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField]
        private Transform ReadyScreen;

        [SerializeField]
        private Transform RunningScreen;

        [SerializeField]
        private Transform GameOverScreen;

        [SerializeField]
        private Slider Player1Health;

        [SerializeField]
        private Slider Player2Health;

        [SerializeField]
        private Sprite WinText;

        [SerializeField]
        private Sprite LoseText;

        [SerializeField]
        private Image Player1GameOverStatus;

        [SerializeField]
        private Image Player2GameOverStatus;

        private void Awake()
        {
            GameManager.Instance.OnGameStateChangedCallback += (state) =>
            {
                UpdateState(state);
            };
        }

        private void UpdateState(GameManager.State state)
        {
            if (state == GameManager.State.READY)
            {
                ReadyScreen.gameObject.SetActive(true);
            }
            else if (state == GameManager.State.RUNNING)
            {
                ReadyScreen.gameObject.SetActive(false);
                RunningScreen.gameObject.SetActive(true);
            }
            else if (state == GameManager.State.GAMEOVER)
            {
                RunningScreen.gameObject.SetActive(false);
                GameOverScreen.gameObject.SetActive(true);

                if (GameManager.Instance.Player1.IsDead())
                {
                    Player1GameOverStatus.sprite = LoseText;
                    Player2GameOverStatus.sprite = WinText;
                }
                else
                {
                    Player1GameOverStatus.sprite = WinText;
                    Player2GameOverStatus.sprite = LoseText;
                }
            }
        }

        private void Update()
        {
            Player1Health.value = GameManager.Instance.Player1.Health / GameManager.Instance.Player1.MaxHealth;
            Player2Health.value = GameManager.Instance.Player2.Health / GameManager.Instance.Player2.MaxHealth;
        }
    }
}
