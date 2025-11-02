using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    public GameObject ballPrefab; // create object to assign ball prefab in Unity
    public GameObject[] ballPrefabs; // create array to add balls in Unity

    // Start should no longer be called ==> handled by the game manager
    // void Start()
    // {
    //     SpawnNewBall();
    // }

    public void SpawnNewBall()
    {
        // Get a random index from our ball prefab array, and use the random ball selected to spawn
        int randIndex = Random.Range(0, ballPrefabs.Length);

        GameObject selectedBall = ballPrefabs[randIndex]; // Pick random ball
        Instantiate(selectedBall, transform.position, selectedBall.transform.rotation); // instantiate new ball
        Debug.Log("Spawned: " + selectedBall.name); // control log ball number
    }
}
