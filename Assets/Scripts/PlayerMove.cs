using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Reference to the camera for movement.
    CameraScript cam;

    // How fast we move, and modifiers for sprinting and ADS.
    public float SpeedModifier = 5f;
    public float TopSpeed = 5f;
    public float SprintMod = 1.2f;
    public float AdsMod = 0.8f;
    public float JumpPower;
    GameManager GM;


    // I should probably start adding some stuff like RigidBody components and animators. Those will go here.
    Rigidbody rb;
    public bool isGrounded = true;
    public bool RocketJumping = false;
    public GameObject GroundRCP;


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
        //Before we move, gonna do myself a favor and call these now. All relevant Axes for just movement.
        float ADSVal = Input.GetAxis("ADS");
        float SprintVal = Input.GetAxis("Sprint");
        float JumpVal = Input.GetAxis("Jump");

        // Improved ground detection here
        

        // Uses a ternary operator to check if we are aiming down sights.
        // I could just put the Input.GetAxis in here and save space but it's not crucial yet.
        cam.SetADS(ADSVal > 0 ? true : false);

        float tempX = Input.GetAxisRaw("Horizontal");
        float tempY = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(tempX) + Mathf.Abs(tempY) > 0 && isGrounded) // If we *are* trying to move...
        {
            // Convert our two input axes into a Vector3 so we can easily apply it to the character.
            var tempInput = (new Vector3(tempX, 0, tempY)).normalized;

            // We must multiply the Vector3 by the int due to how matrix math works
            var moveDir = cam.GetRotation() * tempInput;

            // Modify speed based on Sprint Status unless we are using ADS
            moveDir = SprintVal > 0 && ADSVal == 0 ? moveDir * SprintMod : moveDir;
            // Modify speed for ADS, slowing us down even if we had sprint enabled.
            moveDir = ADSVal > 0 ? moveDir * AdsMod : moveDir;

            // We want to basically kneecap our inertia if we're trying to move in another direction
            if (Vector3.Dot(moveDir, rb.velocity) < 0)
            {
                moveDir *= 2;
            }
            // Now that everything has been calculated, we should apply the force...
            rb.AddForce(moveDir * SpeedModifier);

            var calcTop = TopSpeed;
            if (ADSVal > 0)
            {
                calcTop = TopSpeed * AdsMod;
            }
            else if (SprintVal > 0)
            {
                calcTop = TopSpeed * SprintMod;
            }
            // Now, we need to actually check and make sure we aren't going too fast, provided we're not rocket jumping of course.
            // But jumping should NOT affect our X and Z movement.
            var tempVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            if (!RocketJumping && tempVel.magnitude > calcTop)
            {
                rb.velocity = new Vector3(tempVel.x, rb.velocity.y, tempVel.z).normalized * calcTop;
            }
            
        }
        // Now if we're not moving and we're on the ground we optimally want to completely stop our movement.
        // Barring that, we want to make the slow down feel natural.
        else if (isGrounded && rb.velocity.magnitude > 1)
        {
            // We do both the velocity and the magnitude, in order to make it a strong yet still not immediate stop
            rb.AddForce(rb.velocity * rb.velocity.magnitude * -1);
            if (rb.velocity.magnitude < 0.15)
                rb.velocity = Vector3.zero;
        }

        // If we're grounded and I jump, jump.
        if (isGrounded && JumpVal > 0)
        {
            rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            isGrounded = false;
        }

        // always look "forward" since this is a TPS.
        transform.rotation = cam.GetRotation();
        Debug.Log(rb.velocity.magnitude);
    }
    // TODO: Refine Grounded thing to actually check if it's the ground in question.
    private void OnCollisionStay(Collision thing)
    {
        isGrounded = true;
        RocketJumping = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        switch (tag)
        {
            case "Pit":
                other.GetComponent<PitReactionScript>().SendPlayerToBrazil(gameObject);
                break;
            case "Start":
                GM.ChangeGameState(true);
                break;
            case "End":
                GM.ChangeGameState(false);
                break;
            case "Checkpoint":
                // TO DO: If we even utilize any checkpoint logic, put it here.
                break;
        }
    }
}