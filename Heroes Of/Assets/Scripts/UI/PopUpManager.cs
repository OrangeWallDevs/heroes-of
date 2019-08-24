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

    void Awake() {
        Debug.Log("Método awake do PopUpManager.cs foi chamado");
        Button[] confirmationPrefabButtons = ConfirmationPopUpPrefab.GetComponentsInChildren<Button>();
        this.confirmationYesBtn = confirmationPrefabButtons[0];
        this.confirmationNoBtn = confirmationPrefabButtons[1];
        
        this.warningBtn = WarningPopUpPrefab.GetComponentsInChildren<Button>()[0];
    }

    public void ShowConfirmationModal(string confirmationText, UnityAction yesCallback, UnityAction noCallback) {
        SetupCanvas();

        TextMeshProUGUI confirmationTMPComponent = ConfirmationPopUpPrefab.GetComponentInChildren<TextMeshProUGUI>();
        
        confirmationTMPComponent.text = confirmationText;
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
