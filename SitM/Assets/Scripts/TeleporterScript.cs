using UnityEngine;
using System.Collections;

public class TeleporterScript : MonoBehaviour {

	void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
        }
    }
}
