using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitReactionScript : MonoBehaviour
{
    [SerializeField] Vector3 destination;

    public void SendPlayerToBrazil(GameObject player)
    {
        player.transform.position = destination;
    }
}
