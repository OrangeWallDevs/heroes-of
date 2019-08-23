using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PopUpManager : MonoBehaviour {
    public GameObject WarningPopUpPrefab;
    public GameObject ConfirmationPopUpPrefab;
    private Button confirmationYesBtn, confirmationNoBtn, warningBtn;
    private Canvas sceneCanvas;

    private void Awake() {
        Button[] confirmationPrefabButtons = ConfirmationPopUpPrefab.GetComponentsInChildren<Button>();
        confirmationYesBtn = confirmationPrefabButtons[0];
        confirmationNoBtn = confirmationPrefabButtons[1];
        
        warningBtn = WarningPopUpPrefab.GetComponentsInChildren<Button>()[0];
    }

    public void ShowConfirmationModal(string confirmationText, UnityAction yesCallback, UnityAction noCallback) {
        SetupCanvas();

        TextMeshPro confirmationTMPComponent = ConfirmationPopUpPrefab.GetComponent<TextMeshPro>();
        
        confirmationTMPComponent.SetText(confirmationText);
        confirmationYesBtn.onClick.AddListener(yesCallback);
        confirmationNoBtn.onClick.AddListener(noCallback);

        Instantiate(ConfirmationPopUpPrefab, sceneCanvas.transform);
    }

    public void ShowWarningModal(string warningMessage) {
        SetupCanvas();
        
        TextMeshPro warningTMPComponent = WarningPopUpPrefab.GetComponent<TextMeshPro>();

        warningTMPComponent.SetText(warningMessage);

        Instantiate(WarningPopUpPrefab, sceneCanvas.transform);
    }
    
    private void SetupCanvas() {
        sceneCanvas = FindObjectOfType(typeof(Canvas)) as Canvas;
    }

}
