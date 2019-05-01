using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    GameSession gameStatus;
    public void LoadStartScene()
    {
        
        gameStatus = FindObjectOfType<GameSession>();
        gameStatus.ResetScene();
        SceneManager.LoadScene(0);
    }

    public void LoadCoreScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
