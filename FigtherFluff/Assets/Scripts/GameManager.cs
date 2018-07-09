using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private AnimationCurve hitstunCurve;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Quad").GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 100, ForceMode.Impulse);
            Hitstun(1);
        }
	}

    public void PauseGame()
    {
        Time.timeScale = 1;
    }

    public void Hitstun(float duration)
    {
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
