using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    public static bool IsGamePaused {get; private set;}
    public UIModalOpener PauseMenuOpener;
    public PopUpManager popUpDialog;

    public LevelManager levelTransitor;
    private void Awake() {
        IsGamePaused = false;
    }

    public void TogglePause(GameObject pauseModal) {
        PauseMenuOpener.ToggleModal(pauseModal);
        IsGamePaused = !IsGamePaused;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame() {
        popUpDialog.ShowConfirmationModal("Tem certeza de que deseja sair?", QuitLevel, () => {});
    }

    private void QuitLevel() {
        levelTransitor.LoadScene(1);
    }
 
}
