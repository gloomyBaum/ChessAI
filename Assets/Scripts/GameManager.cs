using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ChessAICore.AISetting aISetting;
    public int difficulty;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;            
        }
        DontDestroyOnLoad(this);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Destroy(this.gameObject);
    }

    public void StartRandomAI()
    {
        aISetting = ChessAICore.AISetting.Random;
        StartCoroutine(StartNewGame());
    }
    public void StartEasyAI()
    {
        aISetting = ChessAICore.AISetting.Easy;
        StartCoroutine(StartNewGame());
    }
    public void StartNormalAI()
    {
        aISetting = ChessAICore.AISetting.Normal;
        StartCoroutine(StartNewGame());
    }
    public void StartHardAI()
    {
        aISetting = ChessAICore.AISetting.Hard;
        StartCoroutine(StartNewGame());
    }
    public IEnumerator StartNewGame()
    {
        SceneManager.LoadScene("ChessGame");
        yield return new WaitForSeconds(0.01f);
        SessionManager.instance.SetupNewGame(aISetting);
    }

    public void StartTicTacToe()
    {
        StartCoroutine(StartNewGameTicTacToe());
    }
    public IEnumerator StartNewGameTicTacToe()
    {
        SceneManager.LoadScene("TicTacToeGame");
        yield return new WaitForSeconds(0.01f);
        TSessionManager.instance.SetUpNewGame();
    }
}
