using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // PUBLIC MOVEMENT VARIABLES: Movement, Jump Power, Sprinting and ADS Speed modifiers, etc.
    public float moveSpeed = 3;
    public float jumpPower = 3;
    public float sprintMod = 1.3f;
    public float ADSMod = 0.6f;

    // INTERNAL VARIABLES: Stuff for getting axes for movement, that sort.
    float movX, movY;

    // Start is called before the first frame update
    void Start()
    {
        // Yeah we want to hide and lock our cursor, probably a good idea.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");
    }
}
