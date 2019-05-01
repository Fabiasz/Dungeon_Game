using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int countObjectsofBlock;
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void IncrementBlockObjects()
    {
        countObjectsofBlock++;
    }
    public void BlockDestroyed()
    {
        countObjectsofBlock--;
        if(countObjectsofBlock == 0)
        {
            sceneLoader.LoadNextScene();
        }
    }

   
}
