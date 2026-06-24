using UnityEngine;

/// <summary>
/// Handles ball physics, player input for aiming and throwing, and force adjustment.
/// Keeps the ball aligned with the arrow until thrown.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    private Transform arrowPos;
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
            arrowPos = arrowController.transform;
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
            arrowPos = arrowController.transform;
        }

        if (arrowPos == null || arrowController == null)
        {
            Debug.LogWarning("Arrow Transform is Null");
            return;
        }

        // Before launch, keep the ball aligned with the aiming arrow.
        Vector3 pos = arrowPos.position;
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

            //Launch the ball with physics force in the arrow's forward direction.
            _rb.AddForce(arrowPos.forward * throwForce, ForceMode.Impulse);
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    public float GetThrowForce()
    {
        return throwForce;
    }
}
