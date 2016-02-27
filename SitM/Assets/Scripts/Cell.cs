using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    public GameObject[] walls = new GameObject[4]; //0 - North, 1 - East, 2 - South, 3 - West
    public int gridX, gridY;
    public bool visted = false;
}
