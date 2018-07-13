using System.Collections;
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

        [SerializeField]
        private AudioClip P1SelectMusic;

        [SerializeField]
        private AudioClip P2SelectMusic;

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

            bool P1Back = Input1.IsButtonDown(InputLayout.ActionType.SPECIAL);
            bool P2Back = Input2.IsButtonDown(InputLayout.ActionType.SPECIAL);


            //foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            //{
            //    if (Input.GetKey(kcode))
            //        Debug.Log("KeyCode down: " + kcode);
            //}

            if (state == State.TITLE)
            {
                if (Input1.IsButtonDown(InputLayout.ActionType.JUMP) || Input2.IsButtonDown(InputLayout.ActionType.JUMP))
                {
                    MusicManager.Instance.PlaySound(P2SelectMusic, 0.6f);
                    TitleScreen.gameObject.SetActive(false);
                    CreditsScreen.gameObject.SetActive(true);
                    state = State.CREDITS;
                }
                else if (Input.anyKeyDown)
                {
                    MusicManager.Instance.PlaySound(P1SelectMusic, 0.6f);
                    TitleScreen.gameObject.SetActive(false);
                    SelectScreen.gameObject.SetActive(true);
                    state = State.SELECT;
                }
            }
            else if (state == State.SELECT)
            {
                if (P1Back || P2Back)
                {
                    TitleScreen.gameObject.SetActive(true);
                    SelectScreen.gameObject.SetActive(false);
                    state = State.TITLE;
                    if (P1Back)
                        MusicManager.Instance.PlaySound(P1SelectMusic, 0.6f);
                    else
                        MusicManager.Instance.PlaySound(P2SelectMusic, 0.6f);
                    return;
                }

                if (Input1.GetAxis(InputLayout.ActionType.MOVE_RIGHT) < -0.2f)
                {
                    var targetPos = FluffSelect.position;
                    targetPos.y = P1Select.position.y;
                    P1Select.position = targetPos;

                    if (P1Selectd != -1)
                    {
                        P1Selectd = -1;
                        MusicManager.Instance.PlaySound(P1SelectMusic, 0.6f);
                    }
                }
                else if (Input1.GetAxis(InputLayout.ActionType.MOVE_RIGHT) > 0.2f)
                {
                    var targetPos = UmbrellaSelect.position;
                    targetPos.y = P1Select.position.y;
                    P1Select.position = targetPos;
                    if (P1Selectd != 1)
                    {
                        P1Selectd = 1;
                        MusicManager.Instance.PlaySound(P1SelectMusic, 0.6f);
                    }
                }

                if (Input2.GetAxis(InputLayout.ActionType.MOVE_RIGHT) < -0.2f)
                {
                    var targetPos = FluffSelect.position;
                    targetPos.y = P2Select.position.y;
                    P2Select.position = targetPos;
                    if (P2Selectd != -1)
                    {
                        P2Selectd = -1;
                        MusicManager.Instance.PlaySound(P2SelectMusic, 0.6f);
                    }
                }
                else if (Input2.GetAxis(InputLayout.ActionType.MOVE_RIGHT) > 0.2f)
                {
                    var targetPos = UmbrellaSelect.position;
                    targetPos.y = P2Select.position.y;
                    P2Select.position = targetPos;
                    if (P2Selectd != 1)
                    {
                        P2Selectd = 1;
                        MusicManager.Instance.PlaySound(P2SelectMusic, 0.6f);
                    }
                }

                if (P1Selectd != P2Selectd)
                {
                    //FluffSelect.parent.GetComponent<Outline>().effectColor = Color.black;
                    // UmbrellaSelect.parent.GetComponent<Outline>().effectColor = Color.black;

                    var p1Target = P1Selectd == -1 ? FluffSelect.parent : UmbrellaSelect.parent;
                    if (P1Selectd != 0)
                    {
                        // p1Target.GetComponent<Outline>().effectColor = Color.blue;
                    }

                    var p2Target = P2Selectd == -1 ? FluffSelect.parent : UmbrellaSelect.parent;
                    if (P2Selectd != 0)
                    {
                        // p2Target.GetComponent<Outline>().effectColor = Color.green;
                    }
                }

                if (P1Selectd != 0 && P2Selectd != 0 && P1Selectd != P2Selectd)
                {
                    StartGame.gameObject.SetActive(true);

                    if (Input1.IsButtonDown(InputLayout.ActionType.START) || Input2.IsButtonDown(InputLayout.ActionType.START))
                    {
                        MusicManager.Instance.PlaySound(P1SelectMusic, 0.6f);

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
                if (P1Back || P2Back)
                {
                    TitleScreen.gameObject.SetActive(true);
                    CreditsScreen.gameObject.SetActive(false);
                    state = State.TITLE;
                    if (P1Back)
                        MusicManager.Instance.PlaySound(P1SelectMusic, 0.6f);
                    else
                        MusicManager.Instance.PlaySound(P2SelectMusic, 0.6f);
                    return;
                }
            }

        }

    }
}
