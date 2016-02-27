using UnityEngine;
using System.Collections;

public class TeleporterScript : MonoBehaviour {

	void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.gm.EndGame();
            Time.timeScale = 0;
        }
    }
}
