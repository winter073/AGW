using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu, HUD, ControlHUD;
    public bool isPaused = false;
    
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        HUD.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Controls()
    {
        pauseMenu.SetActive(false);
        ControlHUD.SetActive(true);
        
    }
    public void GoBack()
    {
        pauseMenu.SetActive(true);
        ControlHUD.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Pause();
            isPaused = true;
        }
    }
}
