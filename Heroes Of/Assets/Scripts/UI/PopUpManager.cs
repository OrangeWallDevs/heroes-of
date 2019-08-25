using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PopUpManager : MonoBehaviour {
    public GameObject WarningPopUpPrefab;
    public GameObject ConfirmationPopUpPrefab;
    private Canvas sceneCanvas;
    
    public void ShowConfirmationModal(string confirmationText, UnityAction yesCallback, UnityAction noCallback) {
        SetupCanvas();

        GameObject confirmationPopUp = Instantiate(ConfirmationPopUpPrefab, sceneCanvas.transform);
        TextMeshProUGUI confirmationTMPComponent = confirmationPopUp.GetComponentInChildren<TextMeshProUGUI>();
        Button[] confirmationPrefabButtons = confirmationPopUp.GetComponentsInChildren<Button>();
        Button confirmationYesBtn = confirmationPrefabButtons[0];
        Button confirmationNoBtn = confirmationPrefabButtons[1];
        
        confirmationTMPComponent.text = confirmationText;

        confirmationYesBtn.onClick.AddListener(() => {
            yesCallback();
            CloseModalInstance(confirmationPopUp);
        });

        confirmationNoBtn.onClick.AddListener(() => {
            noCallback();
            CloseModalInstance(confirmationPopUp);
        });
    }

    public void ShowWarningModal(string warningMessage) {
        SetupCanvas();
        
        GameObject warningPopUp = Instantiate(WarningPopUpPrefab, sceneCanvas.transform);
        TextMeshProUGUI warningTMPComponent = warningPopUp.GetComponentInChildren<TextMeshProUGUI>();
        Button warningRecievedBtn = warningPopUp.GetComponentInChildren<Button>();

        warningTMPComponent.text = warningMessage;

        warningRecievedBtn.onClick.AddListener(() => CloseModalInstance(warningPopUp));
    }
    
    private void SetupCanvas() {
        sceneCanvas = FindObjectOfType(typeof(Canvas)) as Canvas;
    }

    private void CloseModalInstance(GameObject modalInstance) {
        Destroy(modalInstance);
    }
}
