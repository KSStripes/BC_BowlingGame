using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int totalScore = 0;

    public List<int> throws = new List<int>();

    public int[] frameScore;

    private int throwsSinceLastFrame = 0;

    private bool strike = false;

    public void AddThrow(int pins)
    {
        throws.Add(pins);
        throwsSinceLastFrame++;
    }

    int first = 0;
    int second = 0;

    public void FinishFrame(int frameIndex)
    { 
        first = throws[throws.Count - throwsSinceLastFrame];

        if (throwsSinceLastFrame > 1)
        {
            throwsSinceLastFrame--;
            second = throws[throws.Count - throwsSinceLastFrame];

            if (first + second == 10)
            {
                Debug.Log("Spare!");
            }
        }

        frameScore[frameIndex - 1] = first + second;

        if (strike)
        {
            frameScore[frameIndex - 2] = frameScore[frameIndex - 1] + frameScore[frameIndex - 2];
            strike = false;
        }

        if (first == 10)
        {
            Debug.Log("Strike!");
            strike = true;
        }

        totalScore += frameScore[frameIndex];

        // Reset Counters
        first = 0;
        second = 0;
        throwsSinceLastFrame = 0;
    }
}