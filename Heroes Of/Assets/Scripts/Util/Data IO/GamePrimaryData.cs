using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

[CreateAssetMenu]
public class GamePrimaryData : ScriptableObject {

    // Services:
    HTTP http;

    // Final attributes:
    const string PrimaryDataFileName = "PrimaryData.json";
    string primaryDataFilePath;

    void OnEnable() {
        http = HTTP.Instance;
        primaryDataFilePath = $"{Application.persistentDataPath}/{PrimaryDataFileName}";
    }

    public void LoadLocal(Action onDataLoaded) {
        Debug.Log("Loading primary data from local storage");

        if (File.Exists(primaryDataFilePath)) {
            string fileText = File.ReadAllText(primaryDataFilePath);

            Load(fileText, onDataLoaded);
        }

        throw new FileNotFoundException($"File \"{primaryDataFilePath}\" was not found");
    }

    public void LoadFromServer(Action onDataLoaded) {
        Debug.Log("Loading primary data from server");

        http.Post("http://localhost:8080/HeroesOfServer/getGamePrimaryData", (up, down) => {
            string responseText = down.text;

            File.WriteAllText(primaryDataFilePath, responseText);
            Load(responseText, onDataLoaded);
        });
    }

    void Load(String jsonUnarrangedData, Action onDataLoaded) {
        var unarrangedData = JsonConvert
                .DeserializeObject<Dictionary<string, Dictionary<string, List<object>>>>(jsonUnarrangedData);

        foreach (var table in unarrangedData) {
            string tableName = table.Key;
            IEnumerable<string> primaryKeys = table.Value["primaryKeys"].Select(i => ToString()),
                                records = table.Value["records"].Select(i => ToString());
        }

        onDataLoaded();
    }

}
