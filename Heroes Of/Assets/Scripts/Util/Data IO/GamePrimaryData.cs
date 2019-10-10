using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
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
    public List<AssetFilter> AssetFilterRecords { get; set; }
    public List<Part> PartRecords { get; set; }
    public List<Phase> PhaseRecords { get; set; }
    public List<GameUser> UserRecords { get; set; }
    public List<Score> ScoreRecords { get; set; }
    public List<Troop> TroopRecords { get; set; }
    public List<Barrack> BarrackRecords { get; set; }
    public List<Tower> TowerRecords { get; set; }
    public List<Hero> HeroRecords { get; set; }
    public List<Skill> SkillRecords { get; set; }
    public List<Cutscene> CutsceneRecords { get; set; }
    public List<Scene> SceneRecords { get; set; }
    public List<Speak> SpeakRecords { get; set; }

    // Methods:

    void OnEnable() {
        http = HTTP.Instance;
        dataUtil = DataUtil.Instance;
        primaryDataFilePath = $"{Application.persistentDataPath}/{PrimaryDataFileName}";

        // Initialize Data collections:
        AssetFilterRecords = new List<AssetFilter>();
        PartRecords = new List<Part>();
        PhaseRecords = new List<Phase>();
        UserRecords = new List<GameUser>();
        ScoreRecords = new List<Score>();
        TroopRecords = new List<Troop>();
        BarrackRecords = new List<Barrack>();
        TowerRecords = new List<Tower>();
        HeroRecords = new List<Hero>();
        SkillRecords = new List<Skill>();
        CutsceneRecords = new List<Cutscene>();
        SceneRecords = new List<Scene>();
        SpeakRecords = new List<Speak>();
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

        http.Post("http://localhost:8080/heroes-of-server/getPrimaryData", (up, down) => {
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
            IEnumerable<string> primaryKeys = table.Value["primaryKeys"].Select(i => ToString());
            List<JObject> records = table.Value["records"].Cast<JObject>().ToList();

            foreach (var record in records) {
                switch (tableName) {
                    case "assetfilter":
                        AssetFilter assetFilter = record.ToObject<AssetFilter>();
                        AssetFilterRecords.Add(assetFilter);
                        break;
                    case "score": // only current user scores are retrieved
                        Score score = record.ToObject<Score>();
                        ScoreRecords.Add(score);
                        break;
                    case "part":
                        Part part = record.ToObject<Part>();
                        PartRecords.Add(part);
                        break;
                    case "phase":
                        Phase phase = record.ToObject<Phase>();
                        phase.Part = PartRecords
                            .SingleOrDefault(i => i.CodPart == phase.CodPart);
                        phase.UserScore = ScoreRecords
                            .SingleOrDefault(i => i.NumPhase == phase.NumPhase);
                        PhaseRecords.Add(phase);
                        break;
                    case "gameuser": // only current user data is retrieved
                        GameUser user = record.ToObject<GameUser>();
                        user.CurrentPhase = PhaseRecords
                            .SingleOrDefault(i => i.NumPhase == user.NumCurrentPhase);
                        UserRecords.Add(user);
                        break;
                    case "troop":
                        Troop troop = record.ToObject<Troop>();
                        TroopRecords.Add(troop);
                        break;
                    case "barrack":
                        Barrack barrack = record.ToObject<Barrack>();
                        barrack.Part = PartRecords
                            .SingleOrDefault(i => i.CodPart == barrack.CodPart);
                        barrack.Troop = TroopRecords
                            .SingleOrDefault(i => i.CodTroop == barrack.CodTroop);
                        BarrackRecords.Add(barrack);
                        break;
                    case "tower":
                        Tower tower = record.ToObject<Tower>();
                        TowerRecords.Add(tower);
                        break;
                    case "skill":
                        Skill skill = record.ToObject<Skill>();
                        SkillRecords.Add(skill);
                        break;
                    case "hero":
                        Hero hero = record.ToObject<Hero>();
                        hero.Part = PartRecords
                            .SingleOrDefault(i => i.CodPart == hero.CodPart);
                        hero.Skills = SkillRecords
                            .Where(i => i.CodHero == hero.CodHero)
                            .ToList();                        
                        HeroRecords.Add(hero);
                        break;
                    case "speak":
                        Speak speak = record.ToObject<Speak>();
                        SpeakRecords.Add(speak);
                        break;
                    case "scene":
                        Scene scene = record.ToObject<Scene>();
                        scene.Texts = SpeakRecords
                            .Where(i => i.CodCutscene == scene.CodCutscene && i.CodScene == scene.CodScene)
                            .ToList();
                        SceneRecords.Add(scene);
                        break;
                    case "cutscene":
                        Cutscene cutscene = record.ToObject<Cutscene>();
                        cutscene.Scenes = SceneRecords
                            .Where(i => i.CodCutscene == cutscene.CodCutscene)
                            .ToList();
                        CutsceneRecords.Add(cutscene);
                        break;
                    default:
                        break;
                }
            }
        }

        onDataLoaded();
    }

}
