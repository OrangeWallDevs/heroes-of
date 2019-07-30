using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;

public class AppInitialization : MonoBehaviour {
    
    void Awake() {
        // Create client configuration
        PlayGamesClientConfiguration config =
                new PlayGamesClientConfiguration.Builder().Build();

        // Enable debugging output
        PlayGamesPlatform.DebugLogEnabled = true;
        
        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate(); 
    }

}
