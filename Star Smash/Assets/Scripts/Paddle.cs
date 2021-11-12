using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Constraints")]
    // Config Parameters
    public float maxPositionX;
    public float minPositionX;

    [Header("Screen Width & Paddle Position")]
    public float screenWidthInUnits;
    public float paddlePositionY;

    // cashed references
    GameSession myGameSession;
    Ball myBall;

    void Start()
    {
        myGameSession = FindObjectOfType<GameSession>();
        myBall = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePosition = new Vector2(transform.position.x, paddlePositionY);
        paddlePosition.x = Mathf.Clamp(GetXPos(), minPositionX, maxPositionX);
        transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        if (myGameSession.AutoPlayEnabler())
        {
            return myBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
