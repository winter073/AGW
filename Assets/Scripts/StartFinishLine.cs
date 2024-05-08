using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFinishLine : MonoBehaviour
{
    [SerializeField] bool StartLine; // The designer sets this, if it's the Start Line, this is true.
    GameManager GM;
    // Upon existing, find the Game Manager. Things won't work without it.
    void Start()
    {
        GM = GameObject.Find("UI").GetComponent<GameManager>();
    }


    // When the player enters our trigger, send a GameState update to what this object's StartLine variable is.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GM.ChangeGameState(StartLine);
        
    }
}
