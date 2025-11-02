using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class ArrowController : MonoBehaviour
{
    public float moveSpeed = 1;

    public Transform ArrowPos;

    private Animator _animator;

    private float hMove;

    public bool ballThrown = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        ArrowPos = FindAnyObjectByType<ArrowController>().transform;
    }

    private void Update()
    {
        _animator.SetBool("Thrown", ballThrown);


        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (ballThrown) return;

        hMove = Input.GetAxis("Horizontal");

        if( (transform.position.z > 0.44 && hMove < 0) || (transform.position.z < -0.44 && hMove > 0) )
        {
            return;
        } 

        transform.position += Vector3.forward * -1 * moveSpeed * hMove * Time.deltaTime;
    }
}