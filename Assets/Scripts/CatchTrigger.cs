using UnityEngine;

public class CatchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject, 2f); // make ball disappear after 2s
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.BallReachedReturn();
            }
        }
    }
}
