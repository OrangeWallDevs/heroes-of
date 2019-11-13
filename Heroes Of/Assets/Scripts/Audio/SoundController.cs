using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour { 
    public AudioCategory audioCategory;
    private AudioSourceController sceneSongController;
    public AudioData sceneSong;
    
    public GameEvent gameEndEvent;

    void Start() {
        if(!audioCategory.IsMuted) {
           sceneSongController = sceneSong.Play();
        }

        gameEndEvent.RegisterListener(stopSceneSong);
    }

    public void stopSceneSong() {
        sceneSongController.Stop();
    }
}
