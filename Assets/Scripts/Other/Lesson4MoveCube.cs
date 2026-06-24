using Unity.Mathematics;
using UnityEngine;

public class Lesson4MoveCube : MonoBehaviour
{
    // Declare variables for movement
    //public Vector3 MoveAmount;
    public int MoveSpeed = 5;
    public int RotationSpeed = 15;

    public float hMove;
    public float vMove;

    // Declare variables for surface
    public MeshRenderer meshRenderer;
    public Material mat;

    // Start is called once before the first execution of Update 
    void Start()
    {
        // check if we have a mesh renderer
        if (meshRenderer != null)
        {
            // assign material at start
             meshRenderer.material = mat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");

        transform.localPosition += transform.forward * vMove * MoveSpeed * Time.deltaTime;
        transform.localPosition += transform.right * hMove * MoveSpeed * Time.deltaTime;

        // // move forward when Z (=W)
        // if (Input.GetKey(KeyCode.W))
        // {
        //     transform.localPosition += transform.forward * MoveSpeed * Time.deltaTime;
        // }
        // // move backwards when S
        // if (Input.GetKey(KeyCode.S))
        // {
        //     transform.localPosition += transform.forward * -1 * MoveSpeed * Time.deltaTime;
        // }
        // //  Rotate right when D
        // if (Input.GetKey(KeyCode.D))
        // {
        //     transform.localPosition += transform.right * RotationSpeed * Time.deltaTime;
        // }
        //     // Rotate Left When Q (=A)
        // if (Input.GetKey(KeyCode.A))
        // {
        //     transform.localPosition += transform.right * -1 * RotationSpeed * Time.deltaTime;
        // }
    }
}
