using System.Collections;
using UnityEngine;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BallReachedReturn()
    {
        if (waitingForPins || gameOver)
        {
            return;
        }

        StartCoroutine(CheckforFallenPins());
    }


    private IEnumerator CheckforFallenPins()
    {
        waitingForPins = true;
        message = "Checking pins...";
        UpdateUI();

        // Wait briefly so pins have time to settle before scoring.
        yield return new WaitForSeconds(2f);

        int fallenPins = CountFallenPins();
        lastThrowPins = fallenPins;

        if (_ball == 1)
        {
            // Count only the new pins knocked down on the second ball.
            lastThrowPins = Mathf.Max(0, fallenPins - PinsKnockedOver[0]);
        }

        PinsKnockedOver[_ball] = lastThrowPins;
        totalScore += lastThrowPins;

        UpdateUI();

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

    private void FinishRound()
    {
        RoundScore[Round - 1] = PinsKnockedOver[0] + PinsKnockedOver[1];

        if (Round >= totalRounds)
        {
            gameOver = true;
            waitingForPins = false;
            message = "Game over!";
            UpdateUI();
            return;
        }

        Round++;
        _ball = 0;
        ResetPinCount();
        ResetPins();
        message = "New round";
        waitingForPins = false;
        SpawnBallAndResetCamera();
        UpdateUI();
    }

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
