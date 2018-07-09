using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        private enum State
        {
            TITLE,
            SELECT
        }

        private State state = State.TITLE;

        [SerializeField]
        private Transform TitleScreen;

        [SerializeField]
        private Transform SelectScreen;

        [SerializeField]
        private Transform P1Select;

        [SerializeField]
        private Transform P2Select;

        [SerializeField]
        private Transform FluffSelect;

        [SerializeField]
        private Transform UmbrellaSelect;

        [SerializeField]
        private Transform StartGame;

        private int P1Selectd = 0;
        private int P2Selectd = 0;

        private void Update()
        {
            if (state == State.TITLE)
            {
                if (Input.anyKeyDown)
                {
                    TitleScreen.gameObject.SetActive(false);
                    SelectScreen.gameObject.SetActive(true);
                    state = State.SELECT;
                    //  P1Selectd = 0;
                    //P2Selectd = 0;
                }
            }
            else if (state == State.SELECT)
            {
                if (Input.GetButtonDown("P1_Attack") || Input.GetButtonDown("P2_Attack"))
                {
                    TitleScreen.gameObject.SetActive(true);
                    SelectScreen.gameObject.SetActive(false);
                    state = State.TITLE;
                    return;
                }

                if (Input.GetAxis("P1_Horizontal") < -0.4f)
                {
                    var targetPos = FluffSelect.position;
                    targetPos.y = P1Select.position.y;
                    P1Select.position = targetPos;
                    P1Selectd = -1;
                }
                else if (Input.GetAxis("P1_Horizontal") > 0.4f)
                {
                    var targetPos = UmbrellaSelect.position;
                    targetPos.y = P1Select.position.y;
                    P1Select.position = targetPos;
                    P1Selectd = 1;
                }

                if (Input.GetAxis("P2_Horizontal") < -0.4f)
                {
                    var targetPos = FluffSelect.position;
                    targetPos.y = P2Select.position.y;
                    P2Select.position = targetPos;
                    P2Selectd = -1;
                }
                else if (Input.GetAxis("P2_Horizontal") > 0.4f)
                {
                    var targetPos = UmbrellaSelect.position;
                    targetPos.y = P2Select.position.y;
                    P2Select.position = targetPos;
                    P2Selectd = 1;
                }


                if (P1Selectd != 0 && P2Selectd != 0 && P1Selectd != P2Selectd)
                {
                    StartGame.gameObject.SetActive(true);

                    if (Input.anyKey) // TODO
                    {
                        SceneManager.LoadScene(1);
                    }
                }
                else
                {
                    StartGame.gameObject.SetActive(false);
                }

            }

        }
    }
}
