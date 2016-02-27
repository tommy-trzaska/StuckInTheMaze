using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScript : MonoBehaviour {

    public GameObject timer;
    public Text finalTimeText;

    void OnEnable ()
    {
        float finalTime = timer.GetComponent<Timer>().currentTime;
        finalTimeText.text = "Time: "+(int)finalTime / 60 + ":" + (finalTime % 60).ToString("F2");
    }
}
