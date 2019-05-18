using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string nextScene;
    public bool lastSceneWithPlayer = false;
    private bool levelFinished = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!lastSceneWithPlayer)
            {
                DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }
    }

    private void Update()
    {
        if (!levelFinished)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                levelFinished = true;
                transform.GetComponent<SpriteRenderer>().enabled = true;
                transform.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }


    // UnityEngine.SceneManagement.SceneManager.LoadScene(1);
}
