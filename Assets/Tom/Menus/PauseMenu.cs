using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseMenu;
    public bool needToLockCursor = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            switchPause();
        }
    }

    public void switchPause()
    {
        if (paused)
        {
            // unpause
            Time.timeScale = 1;
            paused = false;
            pauseMenu.SetActive(false);
            if (Cursor.lockState == CursorLockMode.Confined && needToLockCursor)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            //pause
            Time.timeScale = 0;
            paused = true;
            pauseMenu.SetActive(true);
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                needToLockCursor = true;
            }
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
