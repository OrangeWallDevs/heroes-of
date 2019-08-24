using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    public static bool IsGamePaused {get; private set;}
    public GameObject PauseMenu;

    private void Awake() {
        IsGamePaused = false;
    }

    public void TogglePause() {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        IsGamePaused = !IsGamePaused;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void RestartGame() {
        Debug.Log("Restarting game...");
    }

    public void QuitGame() {
        Debug.Log("Quiting game...");
    }
}
