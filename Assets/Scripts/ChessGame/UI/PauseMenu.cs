using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        /*
        foreach (Tile tile in ChessBoardManager.Instance.tileList)
        {
            tile.CancelMouseOverHighlight();
        }
        */
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    public void Quit()
    {
        GameIsPaused = false;
        GameManager.Instance.QuitToMainMenu();

    }
}
