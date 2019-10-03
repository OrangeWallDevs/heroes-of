using GooglePlayGames.BasicApi;
using GooglePlayGames;
using System.Collections.Generic;
using UnityEngine;

// public enum GameDataLoadOption {
//     Local, Server
// }

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
        
        HTTPRequestTest();
        PlayGamesConfiguration();
        primaryData.LoadLocal(onDataLoaded);
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

    void HTTPRequestTest() {
        // Debug.Log("Fazendo requisição...");
        // http.Post("http://localhost:8080/HeroesOfServer/getGamePrimaryData", (up, down) => {
        //     Debug.Log("Dados recebidos.");
        //     var response = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<object>>>>(down.text);
        //     Debug.Log(JsonConvert.DeserializeObject<Scene>(response["scene"]["records"][0].ToString()));
        // });
        // Scene scene = new Scene(1, 2, "mensagem");
        // http.Post("http://localhost:8080/HeroesOfServer/addNewScene",
        //         JsonConvert.SerializeObject(scene), (up, down) => {
        //     Debug.Log("Enviado.");
        //     Debug.Log(down.isDone);
        // });
    }

    void LoadPrimaryData() {
        if (!PlayerPrefs.HasKey("firstInitialization")) {
            primaryData.LoadFromServer(() => {
                PlayerPrefs.SetInt("firstInitialization", 0);
                PlayerPrefs.Save();
                onDataLoaded();
            });
        } else {
            primaryData.LoadLocal(onDataLoaded);
        }        
    }

    void onDataLoaded() {
        runtimeData.Load(primaryData);
    }

}
