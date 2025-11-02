using UnityEngine;

public class Pin : MonoBehaviour
{
    public float tiltThreshold = 30f; //check for degrees, when reached pin is knocked over
    private Vector3 _startPosition; // spawn point for reset
    private void Awake()
    {
        _startPosition = transform.position; // save position on scene load
    }

    public bool IsPinKnockedOver()
    {
        float angle = Vector3.Angle(Vector3.up, transform.up); // check how tilted the pin is vs upright
        return (angle > tiltThreshold); // if too tilted, it's fallen
    }

    public void ResetPin()
    {
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // stop from spinning
        transform.position = _startPosition; // warp pin back to spawn
        transform.rotation = new Quaternion(0, 0, 0, 0); // reset rotation
    }
}
