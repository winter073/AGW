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
        StartCoroutine("WaitForInput");
    }

    public void HowToPlay()
    {
        Controls.SetActive(true);
        Starting.SetActive(false);
        Main.SetActive(false);
        StartCoroutine("GoBack");
    }

    public void EndMe()
    {
        Application.Quit();
    }

    IEnumerator WaitForInput()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("MainLevel");
        }
        yield return new WaitForSeconds(0.01f);
    }

    IEnumerator GoBack()
    {
        if (Input.anyKey)
        {
            Start();
            yield break;
        }
        yield return new WaitForSeconds(0.01f);
    }
}

