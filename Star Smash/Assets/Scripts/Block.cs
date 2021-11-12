using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Block : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip breakSound;
    public float volume = 0.07f;

    [Header("Particles")]
    public GameObject blockParticles;
    public float particleLifespan = 2f;

    [Header("Damage")]
    public Sprite[] hitSprites;



    // cached references
    Level level;
    GameSession gameStatus;

    // state variables
    public int timesHit; // TODO only serialized for debug

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D()
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite missing from array " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, volume);
        Destroy(gameObject);
        TriggerParticlesVFX();
        level.BlockDestroyed();
    }

    private void TriggerParticlesVFX()
    {
        GameObject sparkles = Instantiate(blockParticles, transform.position, transform.rotation);
        Destroy(sparkles, particleLifespan);
    }
}
