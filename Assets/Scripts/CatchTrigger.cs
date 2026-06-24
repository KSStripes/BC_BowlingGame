using UnityEngine;

/// <summary>
/// Detects when the ball reaches the return area and triggers the next round setup.
/// Removes the ball from the scene after a short delay.
/// </summary>
public class CatchTrigger : MonoBehaviour
{
    //Destroys the ball and signals the game manager that the throw is complete.
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
