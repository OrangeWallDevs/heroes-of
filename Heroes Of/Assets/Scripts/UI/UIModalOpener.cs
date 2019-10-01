using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModalOpener : MonoBehaviour {
    
    public void OpenModal(GameObject uiModal) {
        uiModal.SetActive(true);
    }

    public void CloseModal(GameObject uiModal) {
        uiModal.SetActive(false);
    }

    public void ToggleModal(GameObject uiModal) {
        uiModal.SetActive(!uiModal.activeSelf);
    }
}