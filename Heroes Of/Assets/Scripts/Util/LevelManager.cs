﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public Canvas loadingScreen;

    public void LoadScene(int sceneBuildIndex) {
        StartCoroutine(LoadSceneAsync(sceneBuildIndex));
    }
    
    private IEnumerator LoadSceneAsync(int sceneBuildIndex) {
        AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(sceneBuildIndex);

        sceneLoader.allowSceneActivation = false;

        loadingScreen.enabled = true;

        while(!sceneLoader.isDone) {

            if(sceneLoader.progress == 0.9f) {
                sceneLoader.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
