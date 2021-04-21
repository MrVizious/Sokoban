using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public bool isRunning = false;
    public GameObject pauseMenu;

    private void Start() {
        UpdatePause();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause() {
        isRunning = !isRunning;
        if (isRunning) Resume();
        else Pause();
    }

    public void Pause() {
        isRunning = false;
        UpdatePause();
    }
    public void Resume() {
        isRunning = true;
        UpdatePause();
    }

    private void UpdatePause() {
        pauseMenu.SetActive(!isRunning);
        if (isRunning) Time.timeScale = 1f;
        else Time.timeScale = 0f;
    }
}
