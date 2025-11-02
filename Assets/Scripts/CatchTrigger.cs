using UnityEngine;



public class CatchTrigger : MonoBehaviour
{
    public BallSpawn spawner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject, 2f); // make ball disappear after 2s
            GameManager.Instance.BallReachedReturn();
            //spawner.SpawnNewBall();
        }
    }
}
