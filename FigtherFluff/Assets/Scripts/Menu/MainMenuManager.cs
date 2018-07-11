using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class MainMenuManager : MonoBehaviour
    {
        private enum State
        {
            TITLE,
            SELECT,
            CREDITS
        }

        private State state = State.TITLE;

        [SerializeField]
        private Transform TitleScreen;

        [SerializeField]
        private Transform SelectScreen;

        [SerializeField]
        private Transform CreditsScreen;

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

        public InputLayout Input1 { get; set; }
        public InputLayout Input2 { get; set; }

        private void Start()
        {
            Input1 = JoystickManager.PLAYER1;
            Input2 = JoystickManager.PLAYER2;
        }

        private void Update()
        {
            //foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            //{
            //    if (Input.GetKey(kcode))
            //        Debug.Log("KeyCode down: " + kcode);
            //}

            if (state == State.TITLE)
            {
                if (Input1.IsButtonDown(InputLayout.ActionType.JUMP) || Input2.IsButtonDown(InputLayout.ActionType.JUMP))
                {
                    TitleScreen.gameObject.SetActive(false);
                    CreditsScreen.gameObject.SetActive(true);
                    state = State.CREDITS;
                }
                else if (Input.anyKeyDown)
                {
                    TitleScreen.gameObject.SetActive(false);
                    SelectScreen.gameObject.SetActive(true);
                    state = State.SELECT;
                    //P1Selectd = 0;
                    //P2Selectd = 0;
                }
            }
            else if (state == State.SELECT)
            {
                if (Input1.IsButtonDown(InputLayout.ActionType.SPECIAL) || Input2.IsButtonDown(InputLayout.ActionType.SPECIAL))
                {
                    TitleScreen.gameObject.SetActive(true);
                    SelectScreen.gameObject.SetActive(false);
                    state = State.TITLE;
                    return;
                }

                if (Input1.GetAxis(InputLayout.ActionType.MOVE_RIGHT) < -0.2f)
                {
                    var targetPos = FluffSelect.position;
                    targetPos.y = P1Select.position.y;
                    P1Select.position = targetPos;
                    P1Selectd = -1;
                }
                else if (Input1.GetAxis(InputLayout.ActionType.MOVE_RIGHT) > 0.2f)
                {
                    var targetPos = UmbrellaSelect.position;
                    targetPos.y = P1Select.position.y;
                    P1Select.position = targetPos;
                    P1Selectd = 1;
                }

                if (Input2.GetAxis(InputLayout.ActionType.MOVE_RIGHT) < -0.2f)
                {
                    var targetPos = FluffSelect.position;
                    targetPos.y = P2Select.position.y;
                    P2Select.position = targetPos;
                    P2Selectd = -1;
                }
                else if (Input2.GetAxis(InputLayout.ActionType.MOVE_RIGHT) > 0.2f)
                {
                    var targetPos = UmbrellaSelect.position;
                    targetPos.y = P2Select.position.y;
                    P2Select.position = targetPos;
                    P2Selectd = 1;
                }

                if (P1Selectd != P2Selectd)
                {
                    FluffSelect.parent.GetComponent<Outline>().effectColor = Color.black;
                    UmbrellaSelect.parent.GetComponent<Outline>().effectColor = Color.black;

                    var p1Target = P1Selectd == -1 ? FluffSelect.parent : UmbrellaSelect.parent;
                    if (P1Selectd != 0)
                    {
                        p1Target.GetComponent<Outline>().effectColor = Color.blue;
                    }

                    var p2Target = P2Selectd == -1 ? FluffSelect.parent : UmbrellaSelect.parent;
                    if (P2Selectd != 0)
                    {
                        p2Target.GetComponent<Outline>().effectColor = Color.green;
                    }
                }

                if (P1Selectd != 0 && P2Selectd != 0 && P1Selectd != P2Selectd)
                {
                    StartGame.gameObject.SetActive(true);

                    if (Input1.IsButtonDown(InputLayout.ActionType.START) || Input2.IsButtonDown(InputLayout.ActionType.START))
                    {
                        PlayerPrefs.SetInt("Player1", P1Selectd);
                        PlayerPrefs.SetInt("Player2", P2Selectd);
                        SceneManager.LoadScene(1);
                    }
                }
                else
                {
                    StartGame.gameObject.SetActive(false);
                }

            }
            else if (state == State.CREDITS)
            {
                if (Input1.IsButtonDown(InputLayout.ActionType.SPECIAL) || Input2.IsButtonDown(InputLayout.ActionType.SPECIAL))
                {
                    TitleScreen.gameObject.SetActive(true);
                    CreditsScreen.gameObject.SetActive(false);
                    state = State.TITLE;
                    return;
                }
            }

        }
    }
}
