using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text ScoreText;

    int endingSceneIndex = 3;

    void Start()
    {
        livesText.text = playerLives.ToString();
        ScoreText.text = score.ToString();
    }
    void Awake()
    {
        var numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
           
            
                gameObject.SetActive(false);
                Destroy(gameObject);
            
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        ScoreText.text = score.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    private void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == endingSceneIndex)
        {
            Destroy(gameObject);
        }
    }
    private void ResetGameSession()
    {
        Destroy(gameObject);
        playerLives = 3;
        score = 0;
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
