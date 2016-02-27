using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MinimapScript : MonoBehaviour {

    public GameObject minimap;
    public Text timerText;

    private float timeLeft = 10;

    void Awake ()
    {
        minimap.SetActive(false);
    }

    void Update ()
    {
        if (GameManager.gm.gameState == GameState.RUNNING)
        {
            timerText.text = timeLeft.ToString("F1");

            if (Input.GetMouseButtonDown(0) && timeLeft > 0)
            {
                minimap.SetActive(true);
            }

            if (Input.GetMouseButton(0))
            {
                timeLeft -= Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0) || timeLeft <= 0)
            {
                minimap.SetActive(false);
            }
        }
    }
}
