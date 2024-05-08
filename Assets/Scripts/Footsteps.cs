using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    Rigidbody rb;
    public GameObject footies;
    PlayerMove PM;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        PM = GetComponent<PlayerMove>();
        footies.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude >= 0.75f && PM.isGrounded)
        {
            footies.SetActive(true);
        }
        else
        {
            footies.SetActive(false);
        }
    }
}
