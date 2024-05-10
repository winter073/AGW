using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    // RayCastPoint, shorthanded to RCP.
    [SerializeField] GameObject RCP;
    [SerializeField] Rigidbody playerRB;
    Slider recharge;
    Image rechargeImage;

    CameraScript cam;
    [SerializeField] PlayerMove player;

    // Actual Gun Stuff
    [Header("Actual Gun Stuff")]
    [SerializeField] float ShotgunCooldown = 5.0f;
    [SerializeField] float ShotgunForce = 10;
    [SerializeField] float GunCooldown = 0.6f;
    float gunTimer = 0.0f;
    float ShotgunTimer = 0.0f;

    // AUDIO STUFF HERE //
    public AudioClip gunSound, ShotSound;
    AudioSource AS;


    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        gunTimer = GunCooldown;
        ShotgunTimer = ShotgunCooldown;
        cam = Camera.main.GetComponent<CameraScript>();
        recharge = GameObject.Find("Slider").GetComponent<Slider>();
        rechargeImage = GameObject.Find("Slider/Fill Area/Fill").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        gunTimer += Time.deltaTime;
        ShotgunTimer += Time.deltaTime * (player.isGrounded ? 2.5f : 1);
        recharge.value = Mathf.Clamp(ShotgunTimer/ShotgunCooldown, 0, 1);
        if (ShotgunTimer >= ShotgunCooldown)
        {
// rechargeImage.color = Color.clear;
        }
        // We also need to make sure the RCP is pointed where the camera is pointed. I'm told that's a good idea.
        transform.forward = cam.transform.forward;

        var tempFire = Input.GetAxis("Fire");
        var tempAlt = Input.GetAxis("AltFire");
        if (tempFire > 0 && gunTimer >= GunCooldown)
        {
            AS.PlayOneShot(gunSound);
            gunTimer = 0;
            Vector3 fire = RCP.transform.forward;
            if (Physics.Raycast(RCP.transform.position, fire, out RaycastHit hit, 50f))
            {
                if (hit.collider.gameObject.CompareTag("Target"))
                {
                    hit.collider.gameObject.GetComponent<Target>().BeenHit();
                }
            }
        }
         if (tempAlt > 0 && ShotgunTimer >= ShotgunCooldown)
        {
            player.isGrounded = false;
            player.RocketJumping = true;
            Vector3 fire = RCP.transform.forward;
            playerRB.AddForce(playerRB.velocity * -0.5f, ForceMode.Impulse);
            playerRB.AddForce(fire * -ShotgunForce, ForceMode.Impulse);
            ShotgunTimer = 0;
            recharge.value = 0;
            rechargeImage.color = Color.white;
            AS.PlayOneShot(ShotSound);
        }

    }
    
}
