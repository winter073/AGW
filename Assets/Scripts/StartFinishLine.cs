using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFinishLine : MonoBehaviour
{
    public bool StartLine;
    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("UI").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GM.ChangeGameState(StartLine);
        }
    }
}
