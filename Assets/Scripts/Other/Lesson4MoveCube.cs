using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// A practice script from lesson 4 that moves and rotates a cube based on player input.
/// This is a learning exercise and not used in the main game.
/// </summary>
public class Lesson4MoveCube : MonoBehaviour
{
    // Variables for movement
    public int MoveSpeed = 5;
    public int RotationSpeed = 15;

    public float hMove;
    public float vMove;

    // Variables for surface appearance
    public MeshRenderer meshRenderer;
    public Material mat;

    /// <summary>Assigns the material to the cube at startup.</summary>
    void Start()
    {
        // check if we have a mesh renderer
        if (meshRenderer != null)
        {
            // assign material at start
             meshRenderer.material = mat;
        }
    }

    /// <summary>Reads input and moves/rotates the cube based on WASD input.</summary>
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
