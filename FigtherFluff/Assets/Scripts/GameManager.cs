using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private AnimationCurve hitstunCurve;

    public static GameManager Instance;

    public PlayerController Player1;
    public PlayerController Player2;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        this.Player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        this.Player2 = GameObject.Find("Player2").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player1.IsDead())
        {

        }
        else if (Player2.IsDead())
        {

        }
    }

    public void PauseGame()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
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
