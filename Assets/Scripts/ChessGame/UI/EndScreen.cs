using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public static EndScreen instance;
    public GameObject canvas;
    public GameObject winText;
    public GameObject loseText;
    public GameObject drawText;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public IEnumerator HandleWin()
    {
        canvas.SetActive(true);
        winText.SetActive(true);
        yield return new WaitForSeconds(4f);
        GameManager.Instance.QuitToMainMenu();
    }
    public IEnumerator HandleLose()
    {
        canvas.SetActive(true);
        loseText.SetActive(true);
        yield return new WaitForSeconds(4f);
        GameManager.Instance.QuitToMainMenu();
    }
    public IEnumerator HandleDraw()
    {
        canvas.SetActive(true);
        drawText.SetActive(true);
        yield return new WaitForSeconds(4f);
        GameManager.Instance.QuitToMainMenu();
    }
}
