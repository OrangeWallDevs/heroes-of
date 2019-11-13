using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndSoundHandler : MonoBehaviour {
    public AudioData victoryTheme;
    public AudioData defeatTheme;

    public GameEvent playerWinEvent;
    public GameEvent playerLooseEvent;

    void Start() {
        playerWinEvent.RegisterListener(PlayVictorySong);
        playerLooseEvent.RegisterListener(PlayDefeatSong);
    }

    public void PlayVictorySong() {
        victoryTheme.Play();
    }

    public void PlayDefeatSong() {
        defeatTheme.Play();
    }
}
