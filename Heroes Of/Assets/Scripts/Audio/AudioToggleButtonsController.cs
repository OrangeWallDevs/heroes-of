using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToggleButtonsController : MonoBehaviour {

    public AudioCategory audioCategory;
    public ToggleButtonImageController toggleMuteBtnController;

    void Start() {
        audioCategory.AudioToggleEvent.RegisterListener(toggleMuteBtnController.action);

        if(audioCategory.IsMuted) {
            toggleMuteBtnController.action(true);
        }
    }

    public void ToggleCategoryMute() {
        audioCategory.ToggleMute();
    }
}
