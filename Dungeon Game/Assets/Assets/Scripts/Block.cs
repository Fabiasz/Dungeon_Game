using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : MonoBehaviour
{
    [SerializeField] AudioClip Bounce;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits = 2;
    [SerializeField] Sprite[] hitSpritesImages;
    [SerializeField] float randomFactor = 0.2f;

    Level level;
    GameSession gameStatus;
    GameObject sparkles;

    [SerializeField] int timesHit=0;
    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameSession>();
        CountBreakableBLocks();
    }

    private void CountBreakableBLocks()
    {
        if (tag == "Breakable")
            level.IncrementBlockObjects();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Not getting Componet cause it creates an AudioSource
        // Sound should be played on a camera cause it is te loudest
        // gameObject = this
        //   AudioSource.PlayClipAtPoint(Bounce, Camera.main.transform.position);

        if (tag == "Breakable") // tag of gameObject this
        {
            HanldeHit();
        }
        
    }

    private void ShowNextHitSprite()
    {
        if (hitSpritesImages[timesHit - 1] != null)
            GetComponent<SpriteRenderer>().sprite = hitSpritesImages[timesHit - 1];
        else
            Debug.LogError("SpriteArray null Exception" + gameObject.name); // name is a name from the editor
    }

    private void HanldeHit()
    {
        maxHits = hitSpritesImages.Length + 1;
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void DestroyBlock()
    {
        level.BlockDestroyed();
        gameStatus.AddToScore();
        Destroy(gameObject);
        //Debug.Log(collision.gameObject.name);
        TriggerSparklesVFX();
    }

    private void TriggerSparklesVFX()
    {
        sparkles = Instantiate(blockSparklesVFX , transform.position , transform.rotation);
        Destroy(sparkles , 1f);
    }

   
}
