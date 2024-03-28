using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFinishLine : MonoBehaviour
{
    public bool StartLine; // The designer triggers this, if it's the Start Line, this is true.
    GameManager GM;
    // Upon existing, find the Game Manager. Things won't work without it.
    void Start()
    {
        GM = GameObject.Find("UI").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GM.ChangeGameState(StartLine);
        
    }
}
