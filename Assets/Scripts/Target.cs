using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class Target : MonoBehaviour
{

    GameManager gm;
    GameObject center;

    // Upon existing, find the Game Manager. Things won't work without it.
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    
    public void BeenHit()
    //  This overload assumes accuracy doesn't matter, and therefore we don't care how far away it was from the center.
    {
        gm.TargetDie();
        gameObject.SetActive(false);
    }

}
