using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // RayCastPoint, shorthanded to RCP.
    [SerializeField] GameObject RCP;
    [SerializeField] Rigidbody playerRB;

    CameraScript cam;

    // Actual Gun Stuff
    [Header("Actual Gun Stuff")]
    [SerializeField] float ShotgunCooldown = 5.0f;
    [SerializeField] float ShotgunForce = 10;
    [SerializeField] float GunCooldown = 0.6f;
    [SerializeField] float GunRange = 10;
    float gunTimer = 0.0f;
    float ShotgunTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        gunTimer = GunCooldown;
        ShotgunTimer = ShotgunCooldown;
        cam = Camera.main.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        gunTimer += Time.deltaTime;
        ShotgunTimer += Time.deltaTime;
        // We also need to make sure the RCP is pointed where the camera is pointed. I'm told that's a good idea.
        transform.forward = cam.transform.forward;

        var tempFire = Input.GetAxis("Fire");
        var tempAlt = Input.GetAxis("AltFire");
        if (tempFire > 0 && gunTimer >= GunCooldown)
        {
            gunTimer = 0;
            Vector3 fire = RCP.transform.forward;
            if (Physics.Raycast(RCP.transform.position, fire, out RaycastHit hit, GunRange))
            {
                Debug.Log("Target hit at: " + hit.point);
            }
        }
         if (tempAlt > 0 && ShotgunTimer >= ShotgunCooldown)
        {
            Vector3 fire = RCP.transform.forward;
            playerRB.AddForce(playerRB.velocity * -0.5f, ForceMode.Impulse);
            playerRB.AddForce(fire * -ShotgunForce, ForceMode.Impulse);
            ShotgunTimer = 0;
        }

    }
    
}
