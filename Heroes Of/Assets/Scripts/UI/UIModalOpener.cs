using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModalOpener : MonoBehaviour {

    public GameObject uiModal;
    
    public void OpenModal() {
        uiModal.SetActive(true);
    }

    public void CloseModal() {
        uiModal.SetActive(false);
    }
}