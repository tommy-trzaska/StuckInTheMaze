using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public enum GameState
{
    MENU,
    RUNNING,
    GAME_OVER
}

public class GameManager : MonoBehaviour {

    public GameState gameState;
    public GameObject mainMenuPanel;
    public GameObject aboutPanel;
    public GameObject gameOverPanel;
    public GameObject mazeGenerator;
    public GameObject timer;
    public FirstPersonController player;

    public static GameManager gm;

    void Awake ()
    {
        gm = this;
        gameState = GameState.MENU;
        mainMenuPanel.SetActive(true);
        aboutPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        timer.SetActive(false);
        player.enabled = false;
    }

    public void StartGame ()
    {
        gameState = GameState.RUNNING;
        mainMenuPanel.SetActive(false);
        mazeGenerator.GetComponent<MazeGenerator>().Setup();
        timer.SetActive(true);
        player.enabled = true;
    }

    public void SeeAboutScreen ()
    {
        mainMenuPanel.SetActive(false);
        aboutPanel.SetActive(true);
    }

    public void ExitButton ()
    {
        Application.Quit();
    }

    public void Return ()
    {
        gameState = GameState.MENU;
        mainMenuPanel.SetActive(true);
        aboutPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void EndGame ()
    {
        gameOverPanel.SetActive(true);
        timer.SetActive(false);
        gameState = GameState.GAME_OVER;
        player.enabled = false;
    }
}
