using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text timerText;
    public float currentTime = 0;

    void Awake ()
    {
        timerText = GetComponent<Text>();
    }

	void Update ()
    {
	    if(GameManager.gm.gameState == GameState.RUNNING)
        {
            currentTime += Time.deltaTime;
        }

        timerText.text = (int)currentTime / 60 + ":" + (currentTime % 60).ToString("F2");
	}
}
