using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[CreateAssetMenu]
public class GamePrimaryData : ScriptableObject {

    // Services:
    HTTP http;
    DataUtil dataUtil;

    // Final attributes:
    const string PrimaryDataFileName = "PrimaryData.json";
    string primaryDataFilePath;

    // Data collections:

    // Methods:

    void OnEnable() {
        http = HTTP.Instance;
        dataUtil = DataUtil.Instance;
        primaryDataFilePath = $"{Application.persistentDataPath}/{PrimaryDataFileName}";
    }

    public void LoadFromDevice(Action onDataLoaded) {
        Debug.Log("Loading primary data from local storage");

        if (File.Exists(primaryDataFilePath)) {
            string fileText = File.ReadAllText(primaryDataFilePath);

            Load(fileText, onDataLoaded);
        }

        throw new FileNotFoundException($"File \"{primaryDataFilePath}\" was not found");
    }

    public void LoadFromServer(Action onDataLoaded) {
        Debug.Log("Loading primary data from server");

        http.Post("http://localhost:8080/heroes-of-server/getGamePrimaryData", (up, down) => {
            string responseText = down.text;
            Debug.Log($"Resposta: {responseText}");

            File.WriteAllText(primaryDataFilePath, responseText);
            Load(responseText, onDataLoaded);
        });
    }

    void Load(String jsonUnarrangedData, Action onDataLoaded) {
        var unarrangedData = JsonConvert
                .DeserializeObject<Dictionary<string, Dictionary<string, List<object>>>>(jsonUnarrangedData);

        foreach (var table in unarrangedData) {
            string tableName = table.Key;
            IEnumerable<string> primaryKeys = table.Value["primaryKeys"].Select(i => ToString());
            List<JObject> records = table.Value["records"].Cast<JObject>().ToList();

            foreach (var record in records) {
                switch (tableName) {
                    case "part":
                        break;
                    case "phase":
                        Logger.Instance.PrintObject(record.ToObject<Phase>());
                        break;
                    case "gameuser":
                        break;
                    default:
                        break;
                }
            }
        }

        onDataLoaded();
    }

}
