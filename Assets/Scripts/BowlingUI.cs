using UnityEngine;

public class BowlingUI : MonoBehaviour
{
    private string displayText = "";

    public void UpdateUI(GameManager game)
    {
        int ballNumber = game._ball + 1;

        displayText =
            "Round: " + game.Round + " / " + game.totalRounds + "\n" +
            "Ball: " + ballNumber + " / 2\n" +
            "Pins this throw: " + game.lastThrowPins + "\n" +
            "Total score: " + game.totalScore + "\n" +
            game.message + "\n\n" +
            "Left / Right: aim\n" +
            "Space: throw\n" +
            "L: restart";
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 230, 170), displayText);
    }
}
