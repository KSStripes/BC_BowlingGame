using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public Transform ArrowPos;

    public float ThrowForce = 50;

    private bool _thrown = false;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        ArrowPos = FindAnyObjectByType<ArrowController>().transform;
        ArrowPos.GetComponent<ArrowController>().ballThrown = false;
    }

    private void Update()
    {
        if (_thrown) return;

        if (ArrowPos == null)
        {
            Debug.LogWarning("Arrow Transform is Null");
            return;
        }



        Vector3 pos = ArrowPos.position;
        pos.x = transform.position.x;
        pos.y = transform.position.y;

        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _thrown = true;
            ArrowPos.GetComponent<ArrowController>().ballThrown = true;
            _rb.AddForce(ArrowPos.forward * ThrowForce, ForceMode.Impulse);
        }
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
