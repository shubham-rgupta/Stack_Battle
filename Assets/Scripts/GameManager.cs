using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager obj;
    public GameObject joystickPanel;
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;
    public int botCount;
    void Awake()
    {
        LeanTween.init(20000);
    }
    private void Start()
    {
        obj = this;
        StartPanel();
    }

    private void Update()
    {
        if (botCount == 0)
        {
            GameWon();
        }
    }
    public void StartPanel()
    {
        startPanel.SetActive(true);
        joystickPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
    }
    public void JoyStickPanel()
    {
        startPanel.SetActive(false);
        joystickPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
        

    }

    public void GameOver()
    {
        startPanel.SetActive(false);
        joystickPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gameWonPanel.SetActive(false);

    }

    public void GameWon()
    {
        startPanel.SetActive(false);
        joystickPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(true);
    }
}
