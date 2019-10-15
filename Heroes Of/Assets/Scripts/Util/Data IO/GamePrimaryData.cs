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
    const string UserDataFileName = "UserData.json";
    string primaryDataFilePath;
    string userDataFilePath;
    GameUser loadedUser;

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
        userDataFilePath = $"{Application.persistentDataPath}/{UserDataFileName}";

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

            Load(fileText, null, onDataLoaded);
        }

        throw new FileNotFoundException($"File \"{primaryDataFilePath}\" was not found");
    }

    public void LoadFromServer(Action onDataLoaded) {
        LoadFromServer(null, onDataLoaded);
    }

    public void LoadFromServer(String userId, Action onDataLoaded) {
        Debug.Log("Loading primary data from server");
        
        var parameters = new Dictionary<string, string>() {
            { "userId", userId }
        };

        http.Post("http://localhost:8080/heroes-of-server/getPrimaryData", parameters, (up, down) => {
            string responseText = down.text;

            File.WriteAllText(primaryDataFilePath, responseText);
            Load(responseText, userId, onDataLoaded);
        });
    }

    void Load(String jsonUnarrangedData, String userId, Action onDataLoaded) {
        var unarrangedData = JsonConvert
                .DeserializeObject<Dictionary<string, Dictionary<string, List<object>>>>(jsonUnarrangedData);

        /* Quando o userId é nulo, isso indica que apenas os dados iniciais são
           carregados, independentemente do usuário */

        ClearRecords();
        
        foreach (var table in unarrangedData) {
            string tableName = table.Key;
            IEnumerable<string> primaryKeys = table.Value["primaryKeys"].Select(i => ToString());
            List<JObject> records = table.Value["records"].Cast<JObject>().ToList();

            foreach (var record in records) {
                AssetFilter filter = AssetFilterRecords
                    .SingleOrDefault(i => i.NamTable == tableName);
                
                switch (tableName) {
                    case "assetfilter":
                        AssetFilter assetFilter = record.ToObject<AssetFilter>();
                        assetFilter.TxtAssetPath = $"Assets/{assetFilter.TxtAssetPath}";
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
                        phase.TilemapsGrid = dataUtil
                            .LoadAsset<Grid>($"{filter.TxtAssetFilter} {phase.NumPhase} t:Grid",
                                new[] { filter.TxtAssetPath });
                        break;
                    case "gameuser": // only current user data is retrieved
                        GameUser user = record.ToObject<GameUser>();
                        user.CurrentPhase = PhaseRecords
                            .SingleOrDefault(i => i.NumPhase == user.NumCurrentPhase);
                        UserRecords.Add(user);
                        break;
                    case "troop":
                        Troop troop = record.ToObject<Troop>();
                        troop.GameObject = dataUtil
                            .LoadAsset<GameObject>($"{filter.TxtAssetFilter} {troop.TxtAssetIdentifier} t:GameObject",
                                new[] { filter.TxtAssetPath });
                        TroopRecords.Add(troop);
                        break;
                    case "barrack":
                        Barrack barrack = record.ToObject<Barrack>();
                        barrack.Part = PartRecords
                            .SingleOrDefault(i => i.CodPart == barrack.CodPart);
                        barrack.Troop = TroopRecords
                            .SingleOrDefault(i => i.CodTroop == barrack.CodTroop);
                        barrack.GameObject = dataUtil
                            .LoadAsset<GameObject>($"{filter.TxtAssetFilter} {barrack.Troop.TxtAssetIdentifier} t:GameObject",
                                new[] { filter.TxtAssetPath });
                        BarrackRecords.Add(barrack);
                        break;
                    case "tower":
                        Tower tower = record.ToObject<Tower>();
                        tower.GameObject = dataUtil
                            .LoadAsset<GameObject>($"{filter.TxtAssetFilter} {(tower.IsEnemy ? "enemy" : "player")} t:GameObject",
                                new[] { filter.TxtAssetPath });
                        TowerRecords.Add(tower);
                        break;
                    case "skill":
                        Skill skill = record.ToObject<Skill>();
                        skill.Action = dataUtil
                            .LoadAsset<SkillAction>($"{filter.TxtAssetFilter} {skill.TxtAssetIdentifier} t:SkillAction",
                                new[] { filter.TxtAssetPath });
                        SkillRecords.Add(skill);
                        break;
                    case "hero":
                        Hero hero = record.ToObject<Hero>();
                        hero.Part = PartRecords
                            .SingleOrDefault(i => i.CodPart == hero.CodPart);
                        hero.Skills = SkillRecords
                            .Where(i => i.CodHero == hero.CodHero)
                            .ToList();
                        hero.GameObject = dataUtil
                            .LoadAsset<GameObject>($"{filter.TxtAssetFilter} {hero.TxtAssetIdentifier} t:GameObject",
                                new[] { filter.TxtAssetPath });
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
                        scene.Sprite = dataUtil
                            .LoadAsset<Sprite>(new[] { $"{filter.TxtAssetPath}/{scene.TxtImagePath}" });
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

        if (UserRecords.Count == 1) {
            loadedUser = UserRecords.SingleOrDefault();
        }

        if (!(onDataLoaded is null)) {
            onDataLoaded();
        }
    }

    public GameUser GetUser() {
        if (loadedUser is null) {
            LoadUserData();
        }
        return loadedUser;
    }

    public void LoadUserData() {
        if (File.Exists(userDataFilePath)) {
            string fileText = File.ReadAllText(primaryDataFilePath);
            var data = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(fileText);
            GameUser user = data["user"].ToObject<GameUser>();
            List<Score> scores = data["scores"].ToObject<List<Score>>();

            user.CurrentPhase = PhaseRecords
                .SingleOrDefault(i => i.NumPhase == user.NumCurrentPhase);
            foreach (Phase phase in PhaseRecords) {
                phase.UserScore = scores
                    .SingleOrDefault(i => i.NumPhase == phase.NumPhase);
            }
            loadedUser = user;
        }
    }

    public void SaveUserData(GameUser user) {
        user.NumCurrentPhase = user.CurrentPhase.NumPhase;

        var data = new Dictionary<string, object>() {
            { "user",  user},
            { "scores", PhaseRecords.Select(i => i.UserScore) }
        };

        File.WriteAllText(userDataFilePath, JsonConvert.SerializeObject(data));
    }

    public void ClearRecords() {
        AssetFilterRecords.Clear();
        PartRecords.Clear();
        PhaseRecords.Clear();
        UserRecords.Clear();
        ScoreRecords.Clear();
        TroopRecords.Clear();
        BarrackRecords.Clear();
        TowerRecords.Clear();
        HeroRecords.Clear();
        SkillRecords.Clear();
        CutsceneRecords.Clear();
        SceneRecords.Clear();
        SpeakRecords.Clear();
    }

}
