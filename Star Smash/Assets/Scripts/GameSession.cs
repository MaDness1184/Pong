using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // config param
    [Header("Game Speed")]
    [Range(0.1f, 10f)] public float gameSpeed = 1f;

    [Header("Score")]
    public int pointsPerBlockDestroyed = 83;
    public TextMeshProUGUI scoreText;

    [Header ("Auto Play")]
    [SerializeField] bool autoPlay = false;

    // state variables
    public int currentScore = 0000;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool AutoPlayEnabler()
    {
        return autoPlay;
    }
}
