using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpTest : MonoBehaviour {
    public PopUpManager alertManager;
    private UnityAction yesFn, noFn;

    public void ConfirmationTest() {
        yesFn = ConfirmationSuccess;
        noFn = ConfirmationFailed;
        alertManager.ShowConfirmationModal("Do you wanna choose yes?", yesFn, noFn);
    }

    public void WarningTest() {
        alertManager.ShowWarningModal("I'm warning you. You have to click 'OK'. Now.");
    }
    
    private void ConfirmationSuccess() {
        Debug.Log("The user choose 'yes'. That's nice :)");
    }

    private void ConfirmationFailed() {
        Debug.Log("The user choose 'no'. That's sad :(");
    }

}
