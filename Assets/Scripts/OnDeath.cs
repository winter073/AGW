using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    AudioSource AS;
    public AudioClip[] sounds; 
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void makeSound()
    {
        AS.clip = sounds[Random.Range(0, sounds.Length)];
        AS.Play();
    }
}
