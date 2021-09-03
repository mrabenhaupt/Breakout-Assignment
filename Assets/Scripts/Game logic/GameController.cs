using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Oversees game states, collects points and updates the UI
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    #region Values
    private int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = value.ToString();
        }
    }
    private int round = 1;
    public int Round
    {
        get
        {
            return round;
        }
        set
        {
            round = value;
            roundText.text = value.ToString();
        }
    }
    private int lives = 3;
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
            livesText.text = value.ToString();
        }
    }

    private int brickCount = 0;
    private Vector3 ballStartingPosition = new Vector3(0, 3, 0);
    #endregion

    #region ForeignReferences 
    public Ball ball;
    public Text scoreText;
    public Text roundText;
    public Text livesText;
    public Text startStopText;
    public Camera mainCamera;
    #endregion
    private void Awake()
    {
        Instance = this;
        Cursor.visible = false;
    }

    private void Start()
    {
        EventManager.Instance.onBrickDestruction += OnBrickDestruction;
        EventManager.Instance.onBallDroppedDown += OnBallFellDown;
        ResetLevel();
    }

    //What: Responds to key input for game start and reset
    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            OnBallFellDown();
        }
        if (Input.GetKeyDown("x"))
        {
            ball.LaunchBall();
            startStopText.text = "";
        }
    }

    public void ResetLevel()
    {
        Score = 0;
        Round = 1;
        Lives = 3;
        brickCount = BrickController.Instance.ResetLevel(round);
        ball.ResetBall(ballStartingPosition);
    }

    //Proceeds to next level, resets ball, lives and brick
    public void NextLevel()
    {
        ball.ResetBall(ballStartingPosition);
        
        if(Round < 10)
        {
            Round++;
        }
        
        Lives = 3;
        brickCount = BrickController.Instance.ResetLevel(Round);
        ball.LaunchBall();
    }

    //Triggered by Event or keyInput; 
    public void OnBallFellDown()
    {
        if(lives > 1)
        {
            Lives--;
            ball.ResetBall(ballStartingPosition);
            ball.LaunchBall();
        }
        else
        {
            startStopText.text = "Game over\n Score:" + Score.ToString() + "\n Press X to restart";
            ResetLevel();
        }
    }

    //Triggered by event; Gives Player points and can trigger level switch
    public void OnBrickDestruction()
    {
        Score += 100;
        brickCount--;

        if (brickCount <= 0)
        {
            NextLevel();
        }
    }
}