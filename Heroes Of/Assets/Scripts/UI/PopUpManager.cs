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
        confirmationYesBtn = GameObject.Find("Confirm_YesBtn").GetComponent<Button>();
        confirmationNoBtn = GameObject.Find("Confirm_NoBtn").GetComponent<Button>();
        warningBtn = GameObject.Find("Warning_YesBtn").GetComponent<Button>();
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
