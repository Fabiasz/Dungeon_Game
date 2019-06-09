using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    AudioSource bgc_music;
    [SerializeField]
    Slider Volume;
    public static AudioManager Instance;
    bool isSlider = false;
    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Slider"))
            isSlider = true;
            if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        bgc_music = GetComponent<AudioSource>();
        
        
    }

    private void Update()
    {   
        if(isSlider)
         bgc_music.volume = Volume.value;
    }

}
