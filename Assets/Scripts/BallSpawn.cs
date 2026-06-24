using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject[] ballPrefabs;

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
