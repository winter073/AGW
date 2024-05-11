using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour
{
    public GameObject Main, Controls, Starting;

    public void Start()
    {
        Controls.SetActive(false);
        Starting.SetActive(false);
        Main.SetActive(true);
    }
    public void PlayGame()
    {
        Controls.SetActive(false);
        Starting.SetActive(true);
        Main.SetActive(false);
    }

    public void HowToPlay()
    {
        Controls.SetActive(true);
        Starting.SetActive(false);
        Main.SetActive(false);
    }

    public void EndMe()
    {
        Application.Quit();
    }

    public void LoadTheScene()
    {
        SceneManager.LoadScene("Kenny_Ship_Map");
    }

   public void LeaveControls()
    {
        Controls.SetActive(false);
        Starting.SetActive(false);
        Main.SetActive(true);
    }
}

