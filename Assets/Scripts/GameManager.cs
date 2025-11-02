using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public int Round = 0; // current round number
    public int _ball = 0; // 0 = first ball, 1 = second, ...
    public int[] PinsKnockedOver; // track pins hit per ball
    public int[] RoundScore; // final score per round
    private BallSpawn ballSpawn; // create instance of BallSpawn script object
    public static GameManager Instance; // create instance for single access
    public Pin[] pins; // create array for all 10 pins ==> manually added in Unity


    private void Awake() 
    {
        // set up singleton pattern for treatment of individual pins
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize arrays to prevent IndexOutOfRange errors ==> I added this in code, course added this in Unity
        PinsKnockedOver = new int[2];  // One slot per ball
        RoundScore = new int[10];      // Adjust if you want more than 10 rounds
        // grab BallSpawn ref from scene
        ballSpawn = FindAnyObjectByType<BallSpawn>(); 
    }

    private void Update() 
    {
        // start first round with space key
        if (Input.GetKeyDown(KeyCode.Space) && Round == 0)
        {
            StartNewRound();
        }
    }

    public void BallReachedReturn()
    {
        // start pin check after ball returns
        StartCoroutine(CheckforFallenPins());
    }


    private IEnumerator CheckforFallenPins()
    {
        // give pins time to settle to determine if fallen
        yield return new WaitForSeconds(2f); 

        // count knocked-over pins
        for (int i = 0; i < pins.Length; i++)
        {
            if (pins[i].IsPinKnockedOver())
            {
                PinsKnockedOver[_ball]++;
            }
        }

        // adjust second ball score if needed
        if (_ball == 1)
        {
            PinsKnockedOver[_ball] -= PinsKnockedOver[0];
        }
        //switch balls or start new round
        if (_ball == 1)
        {
            _ball = 0;
            StartNewRound();
        }
        else
        {
            _ball = 1;
            ballSpawn.SpawnNewBall();
        }
    }

    public void StartNewRound()
    {
        if (Round != 0) UpdateScore(); // add previous round score
        ResetPinCount(); // clear pin hit count
        ResetPins(); //stand pins back up
        Round++; // next round
        ballSpawn.SpawnNewBall(); // new ball
    }
    
        private void ResetPins()
    {
        foreach(Pin pin in pins)
        {
            pin.ResetPin(); // reset each pin position
        }
    }

    private void ResetPinCount()
    {
        for(int i = 0; i < PinsKnockedOver.Length; i++)
        {
            PinsKnockedOver[i] = 0; //clear counts
        }
    }

    private void UpdateScore()
    {
        // add both ball's pins to round score
        RoundScore[Round - 1] = PinsKnockedOver[0] + PinsKnockedOver[1];
    }
}
