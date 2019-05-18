using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D Colider)
    {
        if (Colider.gameObject.tag == "Player") ;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }



   // UnityEngine.SceneManagement.SceneManager.LoadScene(1);
}
