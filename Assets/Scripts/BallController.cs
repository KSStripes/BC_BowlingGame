using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public Transform ArrowPos;

    private float throwForce;
    private GameManager gameManager;
    private AudioSource audioSource;
    private ArrowController arrowController;

    private bool _thrown = false;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        arrowController = FindAnyObjectByType<ArrowController>();
        audioSource = GetComponent<AudioSource>();

        if (arrowController != null)
        {
            ArrowPos = arrowController.transform;
            arrowController.ballThrown = false;
        }

        gameManager = GameManager.Instance;

        if (gameManager == null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
        }

        if (gameManager != null)
        {
            throwForce = gameManager.StartingThrowForce;
        }
    }

    private void Update()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;

            if (gameManager == null)
            {
                return;
            }

            throwForce = gameManager.StartingThrowForce;
        }
        
        if (_thrown) return;

        if (arrowController == null)
        {
            arrowController = FindAnyObjectByType<ArrowController>();
        }

        if (arrowController != null)
        {
            ArrowPos = arrowController.transform;
        }

        if (ArrowPos == null || arrowController == null)
        {
            Debug.LogWarning("Arrow Transform is Null");
            return;
        }



        Vector3 pos = ArrowPos.position;
        pos.x = transform.position.x;
        pos.y = transform.position.y;

        transform.position = pos;

        // Up arrow increases force, Down arrow decreases force.
        if (Input.GetKey(KeyCode.UpArrow))
        {
            throwForce += gameManager.ForceChangeSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            throwForce -= gameManager.ForceChangeSpeed * Time.deltaTime;
        }

        throwForce = Mathf.Clamp(throwForce, gameManager.MinThrowForce, gameManager.MaxThrowForce);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _thrown = true;
            arrowController.ballThrown = true;
            _rb.AddForce(ArrowPos.forward * throwForce, ForceMode.Impulse);
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    // method to return throwForce for UI
    public float GetThrowForce()
    {
        return throwForce;
    }

    // check for pin hit by pin tag and print
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pin")
        {
            Debug.Log(collision.gameObject.name);
        }
    }

}
