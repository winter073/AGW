using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class Target : MonoBehaviour
{
    GameManager gm;

    // Upon existing, find the Game Manager. Things won't work without it.
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    
    public void BeenHit()
    //  When we've been shot, tell the GameManager and deactivate ourselves.
    //  If time allows, disable the collider first, add an animation for being hit, THEN deactivate. If we do this, probably reenable the collider right as we deactivate.
    {
        gm.TargetDie();
        gameObject.SetActive(false);
    }

}
