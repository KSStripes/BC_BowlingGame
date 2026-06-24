using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArrowController : MonoBehaviour
{
    public float moveSpeed = 1;

    private Animator _animator;

    private float hMove;

    public bool ballThrown = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("Thrown", ballThrown);

        if (ballThrown) return;

        hMove = Input.GetAxis("Horizontal");

        if( (transform.position.z > 0.44 && hMove < 0) || (transform.position.z < -0.44 && hMove > 0) )
        {
            return;
        } 

        transform.position += Vector3.forward * -1 * moveSpeed * hMove * Time.deltaTime;
    }
}
