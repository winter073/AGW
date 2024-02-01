using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Reference to the camera for movement.
    CameraScript cam;

    // How fast we move, and modifiers for sprinting and ADS.
    public float moveSpeed = 5f;
    public float sprintMod = 1.2f;
    public float adsMod = 0.8f;
    public float jumpPower;

    // I should probably start adding some stuff like RigidBody components and animators. Those will go here.
    Rigidbody rb;
    [SerializeField] bool isGrounded = true;


    // Start is called before the first frame update, and connects to our camera object.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // For this to work, the Camera needs the Main Camera tag in the editor.
        cam = Camera.main.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Before we move, gonna do myself a favor and call these now
        float ADSVal = Input.GetAxis("ADS");
        float SprintVal = Input.GetAxis("Sprint");
        float JumpVal = Input.GetAxis("Jump");

        // Ternary Operators are great why does nobody go over these things in class holy shit
        cam.SetADS(ADSVal > 0 ? true : false);

        float tempX = Input.GetAxis("Horizontal");
        float tempY = Input.GetAxis("Vertical");
        if (Mathf.Abs(tempX) + Mathf.Abs(tempY) > 0)
        {
            var tempInput = (new Vector3(tempX, 0, tempY)).normalized;

            // Fun fact this took 4 days to fix because apparently the order you multiply things
            // determines whether it *can* multiply at all. That was annoying, but makes sense I guess.
            var moveDir = cam.GetRotation() * tempInput;

            // Ternary Operator to modify move speed based on Sprint Status, giving ADS precedent
            moveDir = SprintVal > 0 && ADSVal == 0 ? moveDir * sprintMod : moveDir;
            // Ternary Operator for ADS, slowing us down, even if we had sprint enabled.
            moveDir = ADSVal > 0 ? moveDir * adsMod : moveDir;

            transform.position += moveDir * moveSpeed * Time.deltaTime; // TODO: Change to Force-Based movement.
        }

        // If we're grounded and I jump, jump.
        if (isGrounded && JumpVal > 0)
        {
            rb.AddForce(Vector3.up * jumpPower ,ForceMode.Impulse);
            isGrounded = false;
        }

        // always look "forward" since this is a TPS.
        transform.rotation = cam.GetRotation();
    }
    // TODO: Refine Grounded thing to actually check if it's the ground in question.
    private void OnCollisionEnter(Collision thing)
    {
        isGrounded = true;
    }
}