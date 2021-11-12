// using System;        Random.range included in "using UnityEngine" already, commented out so it doesn't conflict
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Force")]
    // config params
    public float pushX = 0f;
    public float pushY = 15f;

    [Header("Audio")]
    public AudioClip[] ballSounds;

    [Header("Paddle Object")]
    // state
    public Paddle paddle1;
    private Vector2 paddleToBallVector;
    private Vector2 paddlePos;

    public float randomFactor = 0.2f;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRidgedbody2D;

    private bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRidgedbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            BallToPaddleLock();
            LaunchBallOnMouseClick();
        }
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRidgedbody2D.velocity = new Vector2(pushX, pushY);
            hasStarted = true;
        }
    }

    private void BallToPaddleLock()
    {
        paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor),
            Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRidgedbody2D.velocity += velocityTweak;
        }
    }
}
