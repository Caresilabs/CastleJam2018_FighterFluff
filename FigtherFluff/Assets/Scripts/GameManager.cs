using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Player.Fluff;
using Assets.Scripts.Player.UmbrellaMan;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        READY,
        RUNNING,
        GAMEOVER
    }

    [SerializeField]
    private AnimationCurve hitstunCurve;

    [SerializeField]
    private CameraController Player1Camera;

    [SerializeField]
    private CameraController Player2Camera;


    public delegate void OnGameStateChanged(State newState);
    public OnGameStateChanged OnGameStateChangedCallback;

    public static GameManager Instance;

    public PlayerController Player1 { get; private set; }
    public PlayerController Player2 { get; private set; }

    public PlayerController Fluff { get; private set; }
    public PlayerController UmbrellaMan { get; private set; }

    public State GameState { get; private set; }

    private float stateTime;

    public GameManager()
    {
        Instance = this;
    }

    void Awake()
    {
        Time.timeScale = 1;
        ChangeState(State.READY);

        //PlayerPrefs.SetInt("Player1", 1);
        //PlayerPrefs.SetInt("Player2", -1);

        var p1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        var p2 = GameObject.Find("Player2").GetComponent<PlayerController>();

        // Todo cleanup
       // p1.PlayerType = PlayerPrefs.GetInt("Player1", -1) == -1 ? Assets.Scripts.Player.PlayerType.PLAYER1 : Assets.Scripts.Player.PlayerType.PLAYER2;
       // p2.PlayerType = PlayerPrefs.GetInt("Player2", 1) == -1 ? Assets.Scripts.Player.PlayerType.PLAYER1 : Assets.Scripts.Player.PlayerType.PLAYER2;

        // this.Player1.Movement.PlayerCamera.GetComponentInChildren<Camera>().rect = new Rect(PlayerPrefs.GetInt("Player1", -1) == -1 ? 0 : 0.5f, 0, 0.5f, 1);
        // this.Player2.Movement.PlayerCamera.GetComponentInChildren<Camera>().rect = new Rect(PlayerPrefs.GetInt("Player2", -1) == -1 ? 0 : 0.5f, 0, 0.5f, 1);

        this.Fluff = p1;
        this.UmbrellaMan = p2;

        if (PlayerPrefs.GetInt("Player1", -1) == -1)
        {
            Player1 = p1;
            Player2 = p2;

            Player1Camera.Source = p1.transform;
            Player1Camera.Target = p2.transform;

            Player2Camera.Source = p2.transform;
            Player2Camera.Target = p1.transform;
        }
        else
        {
            Player1 = p2;
            Player2 = p1;

            Player1Camera.Source = p2.transform;
            Player1Camera.Target = p1.transform;

            Player2Camera.Source = p1.transform;
            Player2Camera.Target = p2.transform;
        }

        Player1.PlayerCamera = Player1Camera;
        Player2.PlayerCamera = Player2Camera;

        Player1.Input = JoystickManager.PLAYER1;
        Player2.Input = JoystickManager.PLAYER2;

        //Player1.Movement.LockMovement(5);
        //Player2.Movement.LockMovement(5);
    }

    private void ChangeState(State state)
    {
        GameState = state;
        stateTime = 0;
        if (OnGameStateChangedCallback != null)
        {
            OnGameStateChangedCallback(state);
        }
    }

    // Update is called once per frame
    void Update()
    {
        stateTime += Time.deltaTime;

        if (Player1.IsDead())
        {
            ChangeState(State.GAMEOVER);
        }
        else if (Player2.IsDead())
        {
            ChangeState(State.GAMEOVER);
        }

        if (GameState == State.READY)
        {
            if (stateTime > 6)
            {
                ChangeState(State.RUNNING);
            }
        }
        if (GameState == State.GAMEOVER)
        {
            if (Input.anyKeyDown)
            {
                RestartGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        StopCoroutine("HitstunCoroutine");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Hitstun(float duration)
    {
        StopCoroutine("HitstunCoroutine");
        StartCoroutine(HitstunCoroutine(duration));
    }

    private IEnumerator HitstunCoroutine(float duration)
    {
        float stunTime = 0;

        while (stunTime < duration)
        {
            Time.timeScale = hitstunCurve.Evaluate(stunTime / duration);
            stunTime += Time.fixedUnscaledDeltaTime;
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }
        Time.timeScale = 1;
    }
}
