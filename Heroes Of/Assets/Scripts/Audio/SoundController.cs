using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour { 
    public AudioCategory referenceCategory;
    public AudioData song;

    public ToggleButtonImageController toggleMuteBtnController;
    void Start() {
        referenceCategory.AudioToggleEvent.RegisterListener(toggleMuteBtnController.action);
        song.Play();
    }

    public void ToggleMute() {
        referenceCategory.ToggleMute();
    }


}
