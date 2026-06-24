using UnityEngine;

public class Pin : MonoBehaviour
{
    public float tiltThreshold = 30f;
    private Vector3 _startPosition;
    private bool soundPlayed = false;
    private AudioSource audioSource;

    private void Awake()
    {
        _startPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!soundPlayed && IsPinKnockedOver())
        {
            soundPlayed = true;

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    public bool IsPinKnockedOver()
    {
        // A pin is counted as fallen when it tilts past the threshold angle.
        float angle = Vector3.Angle(Vector3.up, transform.up);
        return (angle > tiltThreshold);
    }

    public void ResetPin()
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        soundPlayed = false;
    }
}
