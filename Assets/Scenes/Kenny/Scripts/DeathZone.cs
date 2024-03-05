using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        TransferPlayer();
    }

    public void TransferPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(-3f, 1.6f, -19f);
    }
}
