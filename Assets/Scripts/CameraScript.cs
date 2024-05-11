using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    // Alright, so we need to know what we're looking at exactly, by how much off we are,
    // and how fast the camera should be moving.
    // The mouseMin and mouseMax are for the clamping of our vertical camera axis.
    public Transform Player;
    public Vector3 cameraOffset;
    public Vector3 ADSOffset;
    public float mouseSpeed = 3;
    public float mouseMin, mouseMax;

    float rotateX, rotateY;
    bool ADSActive = false;
    [SerializeField] PauseMenu PM;
    [Header("Audio")]
    [SerializeField] AudioClip RunMusic;
    [SerializeField] AudioClip NotRunMusic;
    [SerializeField] AudioClip RunEndSFX;
    AudioSource Aud;

    void Start()
    {
        // Hide your cursor and center it so everything doesn't break. Standard thing.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Aud = GetComponent<AudioSource>();
        Aud.clip = NotRunMusic;
        Aud.Play();
    }

    // Update is called once per frame, which is a good thing for this, makes it smoother.
    // If this was something else we might want FixedUpdate (and probably could but I don't mind).
    void Update()
    {
        if (!PM.isPaused)
        {
            // We clamp the Vertical mouse so we aren't looking straight up or straight down.
            // Can edit the exact values in the Editor.
            rotateX += Input.GetAxis("Mouse Y") * mouseSpeed;
            rotateX = Mathf.Clamp(rotateX, mouseMin, mouseMax);
            rotateY += Input.GetAxis("Mouse X") * mouseSpeed;

            // Quaternions suck, send tweet.
            var targetRotation = Quaternion.Euler(-rotateX, rotateY, 0);

            // Ternary operator to determine if we should zoom in a bit for  ADS.
            transform.position = Player.position - targetRotation * (ADSActive ? ADSOffset : cameraOffset);
            transform.rotation = targetRotation;
        }
    }

    // A public function to get the camera's RELEVANT rotation information for movement. I'm told there's something called a property in C# that I could try using but this works fine for now.
    public Quaternion GetRotation()
    {
        return Quaternion.Euler(0, rotateY, 0);
    }

    // I said my comments were okay. variable names? look it does the job.
    public void SetADS(bool Jimmy)
    {
        ADSActive = Jimmy;
    }

    public void SetAudio(bool james)
    {
        Aud.clip = (james ? RunMusic : NotRunMusic);
        Aud.Play();
    }
    public void RunEnds()
    {
        Aud.PlayOneShot(RunEndSFX);
    }
}
