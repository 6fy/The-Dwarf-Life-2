using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject PauseMenu;

    private bool isPaused = false;

    void Update()
    {
        if (isPaused)
        {
            cursorVisible();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isPaused = false;
        cursorInvisible();
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Home()
    {
        Resume();
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        Resume();
        SceneManager.LoadScene("Puzzle 1");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void cursorInvisible()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void cursorVisible()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
