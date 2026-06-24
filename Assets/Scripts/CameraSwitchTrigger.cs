using UnityEngine;

// Script to switch cameras when the player enters a trigger zone

public class CameraSwitchTrigger : MonoBehaviour
{
    public Camera mainCamera;
    public Camera pinCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mainCamera == null || pinCamera == null)
            {
                Debug.LogWarning("CameraSwitchTrigger is missing a camera reference.");
                return;
            }

            mainCamera.gameObject.SetActive(false);
            pinCamera.gameObject.SetActive(true);
        }
    }
    public void ResetCamera()
    {
        if (mainCamera == null || pinCamera == null)
        {
            Debug.LogWarning("CameraSwitchTrigger is missing a camera reference.");
            return;
        }

        mainCamera.gameObject.SetActive(true);
        pinCamera.gameObject.SetActive(false);
    }
}
