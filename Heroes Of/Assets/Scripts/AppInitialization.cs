using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections.Generic;
using UnityEngine;

public class AppInitialization : MonoBehaviour {

    public GamePrimaryData primaryData;
    public GameRuntimeData runtimeData;

    HTTP http;

    public bool FirstInitialization {
        get; private set;
    }

    void Start() {
        http = HTTP.Instance;
        PlayerPrefs.DeleteAll(); // just for debugging
        
        PlayGamesConfiguration();
        primaryData.LoadFromServer(onDataLoaded);
        // LoadPrimaryData(); // success callback after user login
        
    }

    void PlayGamesConfiguration() {
        // Create client configuration
        PlayGamesClientConfiguration config =
                new PlayGamesClientConfiguration.Builder().Build();

        // Enable debugging output
        PlayGamesPlatform.DebugLogEnabled = true;
        
        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }

    void LoadPrimaryData() {
        if (!PlayerPrefs.HasKey("firstInitialization")) {
            primaryData.LoadFromServer(() => {
                PlayerPrefs.SetInt("firstInitialization", 0);
                PlayerPrefs.Save();
                onDataLoaded();
            });
        } else {
            primaryData.LoadFromDevice(onDataLoaded);
        }        
    }

    void onDataLoaded() {
        runtimeData.Load(primaryData);
    }

}
