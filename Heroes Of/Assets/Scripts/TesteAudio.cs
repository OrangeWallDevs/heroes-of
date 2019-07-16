using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteAudio : MonoBehaviour {

    public AudioData MusTeste;
    public AudioCategory MusCategory;

    void Start() {
        MusTeste.Play();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            MusCategory.ToggleMute();
        }
    }
}
