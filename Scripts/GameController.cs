using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,Playing,End
}

public class GameController : MonoBehaviour {

    public static GameState gameState = GameState.Menu;
    public GameObject taptostartUI;
    public GameObject gameoverUI;
    public GameObject titleUI;
    private void Update()
    {
        if (gameState == GameState.Menu)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameState = GameState.Playing;
                taptostartUI.SetActive(false);
                titleUI.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
        if (gameState != GameState.End)
        {
            gameoverUI.SetActive(false);
        }
        else
        {
            gameoverUI.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                gameState = GameState.Menu;
                Application.LoadLevel(0);
            }
        }
    }
}

