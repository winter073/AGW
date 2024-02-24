using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Target : MonoBehaviour
{

    GameManager gm;
    GameObject center;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void BeenHit(RaycastHit hit, bool AccuracyEnabled)
    {
        hit.transform.LookAt(center.transform);

        Vector2 dist = new Vector2(0, 0);
    }

    public void BeenHit()
    //  This overload assumes accuracy doesn't matter, and therefore we don't care how far away it was from the center.
    {
        gm.TargetDie();
        gameObject.SetActive(false);
    }

}
