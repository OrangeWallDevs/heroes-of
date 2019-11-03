using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour { 
    public AudioCategory audioCategory;
    public AudioData song;
    void Start() {
        if(!audioCategory.IsMuted) {
            song.Play();
        }
    }
}
