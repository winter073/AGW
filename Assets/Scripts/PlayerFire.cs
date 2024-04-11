using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    // RayCastPoint, shorthanded to RCP.
    [SerializeField] GameObject RCP;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] Slider recharge;
    [SerializeField] Image rechargeImage;

    CameraScript cam;
    [SerializeField] PlayerMove player;

    // Actual Gun Stuff
    [Header("Actual Gun Stuff")]
    [SerializeField] float ShotgunCooldown = 5.0f;
    [SerializeField] float ShotgunForce = 10;
    [SerializeField] float GunCooldown = 0.6f;
    float gunTimer = 0.0f;
    float ShotgunTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        gunTimer = GunCooldown;
        ShotgunTimer = ShotgunCooldown;
        cam = Camera.main.GetComponent<CameraScript>();;
    }

    // Update is called once per frame
    void Update()
    {
        gunTimer += Time.deltaTime;
        ShotgunTimer += Time.deltaTime * (player.isGrounded ? 2.5f : 1);
        recharge.value = Mathf.Clamp(ShotgunTimer/ShotgunCooldown, 0, 1);
        if (ShotgunTimer >= ShotgunCooldown)
        {
            rechargeImage.color = Color.clear;
        }
        // We also need to make sure the RCP is pointed where the camera is pointed. I'm told that's a good idea.
        transform.forward = cam.transform.forward;

        var tempFire = Input.GetAxis("Fire");
        var tempAlt = Input.GetAxis("AltFire");
        if (tempFire > 0 && gunTimer >= GunCooldown)
        {
            gunTimer = 0;
            Vector3 fire = RCP.transform.forward;
            if (Physics.Raycast(RCP.transform.position, fire, out RaycastHit hit, 50f))
            {
                if (hit.collider.gameObject.CompareTag("Target"))
                {
                    Debug.Log("Thing hit is a target.");
                    hit.collider.gameObject.GetComponent<Target>().BeenHit();
                }
                Debug.Log("Thing hit at: " + hit.point);
            }
        }
         if (tempAlt > 0 && ShotgunTimer >= ShotgunCooldown)
        {
            Vector3 fire = RCP.transform.forward;
            playerRB.AddForce(playerRB.velocity * -0.5f, ForceMode.Impulse);
            playerRB.AddForce(fire * -ShotgunForce, ForceMode.Impulse);
            ShotgunTimer = 0;
            recharge.value = 0;
            rechargeImage.color = Color.white;
        }

    }
    
}
