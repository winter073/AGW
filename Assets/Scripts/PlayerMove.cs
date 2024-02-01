using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CameraScript cam;

    // How fast we move, and modifiers for sprinting and ADS.
    public float moveSpeed = 5f;
    public float sprintMod = 1.2f;
    public float adsMod = 0.8f;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Before we move, gonna do myself a favor and call these now
        float ADSVal = Input.GetAxis("ADS");
        float SprintVal = Input.GetAxis("Sprint");

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

            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        // always look "forward" since this is a TPS.
        transform.rotation = cam.GetRotation();
    }
}
