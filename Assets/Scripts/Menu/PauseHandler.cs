using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    private bool isPaused = false;

    public Button resumeButton;
    public Button restartButton;
    public Button exitButton;

    public GameObject pauseMenu;

    public GameObject canvas;
    // Todo: Maybe move this to main game file to detect for esc key press

    // Update is called once per frame

    void Start()
    {
        canvas.SetActive(false);
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        canvas.SetActive(true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // freeze gameplay
        StopAllCoroutines();
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Currently Breaks the game
    }
}
