using UnityEngine;

/// <summary>
/// Displays game stats and controls on screen using the OnGUI system.
/// Shows current round, score, throw force, and player control instructions.
/// </summary>
public class BowlingUI : MonoBehaviour
{
    private string displayText = "";
    private GameManager gameManager;
    private GUIStyle boxStyle;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        if (gameManager != null)
        {
            UpdateUI(gameManager);
        }
    }

    //Updates the display text with current game information.
    public void UpdateUI(GameManager game)
    {
        int ballNumber = game._ball + 1;

        BallController ball = FindAnyObjectByType<BallController>();
        float throwForce = 0f;

        // Get throwForce from BallController and update the UI.
        if (ball != null)
        {
            throwForce = ball.GetThrowForce();
        }

        displayText =
            "<b><color=#00FFFF>Game Statistics:</color></b>\n" +
            "Round: " + game.Round + " / " + game.totalRounds + "\n" +
            "Ball: " + ballNumber + " / 2\n" +
            "<b>Throw Force:</b> " + Mathf.RoundToInt(throwForce) + "\n" +

            "Pins this throw: " + game.lastThrowPins + "\n" +
            "Total score: " + game.totalScore + "\n" +
            game.message + "\n\n" +
            "\n" +
            "<b><color=#00FFFF>Player Controls:</color></b>\n" +
            "Arrow Up / Down: adjust throw force\n" +
            "Left / Right: aim\n" +
            "Space: throw\n" +
            "L: restart";
    }

    private void OnGUI()
    {
        float margin = 16f;
        float panelWidth = Mathf.Clamp(Screen.width * 0.28f, 220f, 300f);
        float panelHeight = Mathf.Clamp(Screen.height * 0.3f, 220f, 250f);

        float x = margin;
        float y = Screen.height - panelHeight - margin;

        if (boxStyle == null)
        {
            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.richText = true;
        }

        GUI.Box(new Rect(x, y, panelWidth, panelHeight), displayText, boxStyle);
    }
}
