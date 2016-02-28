using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScript : MonoBehaviour {

    public GameObject timer;
    public Text finalTimeText;
    public Text bestTimeText;

    void Awake ()
    {
        //PlayerPrefs.DeleteAll();
    }

    public void ShowSummary ()
    {
        float finalTime = timer.GetComponent<Timer>().currentTime;
        finalTimeText.text = "Time: "+(int)finalTime / 60 + ":" + (finalTime % 60).ToString("F2");

        float bestTime = 100000000.0f;
        Debug.Log(bestTime);
        if (!PlayerPrefs.HasKey("BestTime"))
            PlayerPrefs.SetFloat("BestTime", 1000000000.0f);
        
        bestTime = PlayerPrefs.GetFloat("BestTime");
        Debug.Log(bestTime);
        if (bestTime > finalTime)
        {
            PlayerPrefs.SetFloat("BestTime", finalTime);
            bestTime = finalTime;
            Debug.Log(bestTime);
        }
        bestTimeText.text = "Best: " + (int)bestTime / 60 + ":" + (bestTime % 60).ToString("F2");
    }
}
