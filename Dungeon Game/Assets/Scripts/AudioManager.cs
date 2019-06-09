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
    private void Awake()
    {
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
        bgc_music.volume = Volume.value;
    }

}
