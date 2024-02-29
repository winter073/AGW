using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceThePlayer : MonoBehaviour
{

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerModel");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
    }
}
