using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{

    [Range(0.1f, 10.0f)] [SerializeField]  float gameSpeed = 1f;

    [SerializeField] int currentScore = 0;
    [SerializeField] int scorePerBlockDestroyed = 10;
    [SerializeField] Text scoreText;
    [SerializeField] bool isAutoPlayEnabled = false;
    void Start()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length; // objects
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false); //other object for very short moment want to access that object
            // so it needs to be deactivated first 
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public bool AutoPlay()
    {
        return isAutoPlayEnabled;
    }

    void Update()
    {
        Time.timeScale = gameSpeed;
    }
    public void AddToScore()
    {
        currentScore += scorePerBlockDestroyed;
        scoreText.text = "Score: " + currentScore.ToString();
    }
    public void ResetScene()
    {
        Destroy(gameObject);
    }
}
