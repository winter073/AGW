using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitReactionScript : MonoBehaviour
{
    [SerializeField] Vector3 Brazil;
    // If the player goes into this object, it will trigger this function, sending the player to a predetermined location. 
    // Why did I call it "SendPlayerToBrazil"? Good question.
    public void SendPlayerToBrazil(GameObject player)
    {
        player.transform.position = Brazil;
    }
}
