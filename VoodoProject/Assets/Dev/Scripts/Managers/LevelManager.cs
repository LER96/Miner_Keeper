using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public static Action OnLevelWin;
    public static Action OnLevelLose;

    [SerializeField] GameObject _endWindow;
    [SerializeField] Animator _winLoseWindow;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnLevelLose += Lose;
        OnLevelWin += Win;
        _endWindow.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Dev");
    }

    public void Home()
    {
        Application.Quit();
    }

    public void Win()
    {
        _endWindow.SetActive(true);
        _winLoseWindow.Play("Win");
    }

    public void Lose()
    {
        _endWindow.SetActive(true);
        _winLoseWindow.Play("Lose");
    }
}
