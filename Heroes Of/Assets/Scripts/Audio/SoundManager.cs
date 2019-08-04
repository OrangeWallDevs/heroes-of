using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour { 
    public AudioCategory referenceCategory;
    public AudioData song;

    void Start() {
        song.Play();
    }

    public void ToggleMute() {
        referenceCategory.ToggleMute();
    }


}
