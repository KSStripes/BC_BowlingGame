using UnityEngine;

/// <summary>
/// Switches between cameras when the ball enters a trigger zone.
/// Used to show the pins from a different angle when the ball reaches them.
/// </summary>
public class CameraSwitchTrigger : MonoBehaviour
{
    public Camera mainCamera;
    public Camera pinCamera;

    //Switches to the pin camera when the ball enters the trigger.
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

    //Switches back to the main camera.
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
