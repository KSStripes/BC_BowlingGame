using UnityEngine;

/// <summary>
/// Handles spawning a new ball at the start of each throw.
/// Can spawn a single prefab or randomly select from an array of ball prefabs.
/// </summary>
public class BallSpawn : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject[] ballPrefabs;

    //Create a new ball at the spawn position.
    public void SpawnNewBall()
    {
        if (ballPrefabs == null || ballPrefabs.Length == 0)
        {
            if (ballPrefab == null)
            {
                Debug.LogWarning("No ball prefab assigned to BallSpawn.");
                return;
            }

            Instantiate(ballPrefab, transform.position, ballPrefab.transform.rotation);
            return;
        }

        int randIndex = Random.Range(0, ballPrefabs.Length);

        GameObject selectedBall = ballPrefabs[randIndex];
        Instantiate(selectedBall, transform.position, selectedBall.transform.rotation);
    }
}
