using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void BeenHit(RaycastHit hit, bool AccuracyEnabled)
    {

    }

    public void BeenHit(RaycastHit hit)
    //  This overload operates as a default of AccuracyEnabled being false.
    {

    }

}
