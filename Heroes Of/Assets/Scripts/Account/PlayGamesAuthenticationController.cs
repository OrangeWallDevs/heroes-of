using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
public class PlayGamesAuthenticationController : MonoBehaviour {
    public PopUpManager alertManager;
    void Start() {
        PlayGamesClientConfiguration clientConfig = new PlayGamesClientConfiguration.Builder()
        .Build();

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.InitializeInstance(clientConfig);
        PlayGamesPlatform.Activate();
    }

    public void ToggleSignInOut() {
        if (!PlayGamesPlatform.Instance.localUser.authenticated) {
            PlayGamesPlatform.Instance.Authenticate(HandleSignIn, false);
        } else {
            PlayGamesPlatform.Instance.SignOut();
        }
    }

    public void HandleSignIn(bool success) {
        if (success) {
            alertManager.ShowWarningModal("Logado como: " + Social.localUser.userName);
        } else {
            alertManager.ShowWarningModal("Não foi possível se conectar à sua conta!");
        }
    }

    
}