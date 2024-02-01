using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // RayCastPoint, shorthand for RCP.
    [SerializeField] GameObject RCP;
    CameraScript cam;

    // Actual Gun Stuff
    [Header("Actual Gun Stuff")]
    [SerializeField] float ShotgunCooldown = 5.0f;
    [SerializeField] float GunCooldown = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // We also need to make sure the RCP is pointed where the camera is pointed. I'm told that's a good idea.
        transform.forward = cam.transform.forward;

        var tempFire = Input.GetAxis("Fire");
        if (tempFire > 0)
        {
            Vector3 fire = RCP.transform.forward;
            if (Physics.Raycast(RCP.transform.position, fire, out RaycastHit hit, 10))
            {
                Debug.Log("Target hit at: " + hit.point);
            }
        }

    }
    
}
