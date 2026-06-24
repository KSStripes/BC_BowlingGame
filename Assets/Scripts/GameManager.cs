using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the overall game flow, scoring, and round progression for the bowling game.
/// Handles pin counting, round management, and game state.
/// </summary>
public class GameManager : MonoBehaviour
{
    public int Round = 1;
    public int _ball = 0; // 0 = first ball, 1 = second ball
    public int totalRounds = 5;
    public int totalScore = 0;
    public int lastThrowPins = 0;
    public int[] PinsKnockedOver;
    public int[] RoundScore;
    public Pin[] pins;
    public string message = "";
    public bool gameOver = false;

    [Header("Throw Force Settings")]
    [SerializeField] private float startingThrowForce = 90f;
    [SerializeField] private float minThrowForce = 50f;
    [SerializeField] private float maxThrowForce = 110f;
    [SerializeField] private float forceChangeSpeed = 30f;

    private BallSpawn ballSpawn;
    private BowlingUI ui;
    private CameraSwitchTrigger cameraSwitch;
    private bool waitingForPins = false;
    public static GameManager Instance;

    public float StartingThrowForce
    {
        get { return startingThrowForce; }
    }

    public float MinThrowForce
    {
        get { return minThrowForce; }
    }

    public float MaxThrowForce
    {
        get { return maxThrowForce; }
    }

    public float ForceChangeSpeed
    {
        get { return forceChangeSpeed; }
    }

    // Set up the singleton instance and initializes game data.
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PinsKnockedOver = new int[2];
        RoundScore = new int[totalRounds];
        ballSpawn = FindAnyObjectByType<BallSpawn>();
        ui = FindAnyObjectByType<BowlingUI>();
        cameraSwitch = FindAnyObjectByType<CameraSwitchTrigger>();
    }

    private void Start()
    {
        StartGame();
    }

    // Allows restarting the game by pressing L.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    //Called when the ball reaches the return area. Triggers pin checking.
    public void BallReachedReturn()
    {
        if (waitingForPins || gameOver)
        {
            return;
        }

        StartCoroutine(CheckforFallenPins());
    }


    // Waits for pins to settle, counts fallen pins, and determines if a second ball is needed.
    private IEnumerator CheckforFallenPins()
    {
        waitingForPins = true;
        message = "Checking pins...";
        UpdateUI();

        // Wait briefly so pins have time to settle before scoring.
        yield return new WaitForSeconds(2f);

        int fallenPins = CountFallenPins();
        lastThrowPins = fallenPins;

        // On second ball, only count NEW pins knocked over
        if (_ball == 1)
        {
            lastThrowPins = Mathf.Max(0, fallenPins - PinsKnockedOver[0]);
        }

        PinsKnockedOver[_ball] = lastThrowPins;
        totalScore += lastThrowPins;

        UpdateUI();

        // If first ball didn't knock down all pins, player gets a second ball
        if (_ball == 0 && fallenPins < pins.Length)
        {
            _ball = 1;
            message = "Second ball";
            waitingForPins = false;
            SpawnBallAndResetCamera();
            UpdateUI();
            yield break;
        }

        FinishRound();
    }

    private int CountFallenPins()
    {
        int count = 0;

        foreach (Pin pin in pins)
        {
            if (pin.IsPinKnockedOver())
            {
                count++;
            }
        }

        return count;
    }

    // Saves the round score and either starts the next round or ends the game.
    private void FinishRound()
    {
        RoundScore[Round - 1] = PinsKnockedOver[0] + PinsKnockedOver[1];

        // Check if game is over
        if (Round >= totalRounds)
        {
            gameOver = true;
            waitingForPins = false;
            message = "Game over!";
            UpdateUI();
            return;
        }

        // Set up next round
        Round++;
        _ball = 0;
        ResetPinCount();
        ResetPins();
        message = "New round";
        waitingForPins = false;
        SpawnBallAndResetCamera();
        UpdateUI();
    }

    // Resets all game variables to start a fresh game.
    public void StartGame()
    {
        Round = 1;
        _ball = 0;
        totalScore = 0;
        lastThrowPins = 0;
        gameOver = false;
        waitingForPins = false;
        message = "Aim with Left/Right, press Space";
        ResetPinCount();
        ResetRoundScores();
        ResetPins();
        SpawnBallAndResetCamera();
        UpdateUI();
    }

    // Resets the camera view and spawns a new ball for the current throw.
    private void SpawnBallAndResetCamera()
    {
        if (cameraSwitch != null)
        {
            cameraSwitch.ResetCamera();
        }

        ballSpawn.SpawnNewBall();
    }

    private void ResetRoundScores()
    {
        for (int i = 0; i < RoundScore.Length; i++)
        {
            RoundScore[i] = 0;
        }
    }
    
    private void ResetPins()
    {
        foreach(Pin pin in pins)
        {
            pin.ResetPin();
        }
    }

    private void ResetPinCount()
    {
        for(int i = 0; i < PinsKnockedOver.Length; i++)
        {
            PinsKnockedOver[i] = 0;
        }
    }

    private void UpdateUI()
    {
        if (ui != null)
        {
            ui.UpdateUI(this);
        }
    }
}
