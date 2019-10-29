using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class GameRuntimeData : ScriptableObject {

    /* --- Dependencies and Private Members --- */    

    Dictionary<Type, object> gamePrototypeTypes;

    /* --- Runtime Members --- */

    // Initialized by primaryData:
    public List<Part> Parts { get; set; }
    public List<Phase> Phases { get; set; }
    public GameUser User { get; set; }
    public List<Troop> TroopPrototypes { get; set; }
    public List<Barrack> BarrackPrototypes { get; set; }
    public List<Tower> TowerPrototypes { get; set; }
    public List<Hero> HeroPrototypes { get; set; }
    public List<Cutscene> Cutscenes { get; set; }

    // Runtime-only members:
    public List<Troop> RuntimeTroops { get; set; }
    public List<Barrack> RuntimeBarracks { get; set; }
    public List<Tower> RuntimeTowers { get; set; }
    public List<Hero> RuntimeHeros { get; set; }
    public Hero PlayerHero { get; set; }
    public Hero EnemyHero { get; set; }

    // ~ Acrescentar turnos, ouro, slots etc ~

    public Phase CurrentLevel {
        get; private set;
    }

    public NodeTilemap NodeTilemap {
        get; private set;
    }

    /* --- Methods --- */

    void OnEnable() {

        RuntimeTroops = new List<Troop>();
        RuntimeBarracks = new List<Barrack>();
        RuntimeTowers = new List<Tower>();
        RuntimeHeros = new List<Hero>();
        gamePrototypeTypes = new Dictionary<Type, object> {
            { typeof(Troop), RuntimeTroops },
            { typeof(Barrack), RuntimeBarracks },
            { typeof(Tower), RuntimeTowers },
            { typeof(Hero), RuntimeHeros }
        };

    }

    public void Load(GamePrimaryData primaryData) {

        Parts = primaryData.PartRecords;
        Phases = primaryData.PhaseRecords;
        User = primaryData.GetUser(); // mudar para método de verificação do primaryData
        TroopPrototypes = primaryData.TroopRecords;
        BarrackPrototypes = primaryData.BarrackRecords;
        TowerPrototypes = primaryData.TowerRecords;
        HeroPrototypes = primaryData.HeroRecords;
        Cutscenes = primaryData.CutsceneRecords;

    }

    public void StartPhase(Action onFinishLoading) {
        // Carrega o tilemap da fase:
        // Grid levelGrid = Instantiate(dataUtil
        //         .LoadAsset<Grid>("test", new[] {"Assets/Prefabs"}));
        // CurrentLevel = new Phase(levelGrid);
        // NodeTilemap = new NodeTilemap(CurrentLevel.Tilemaps);

        // Debug.Log(NodeTilemap.Nodes.Count);
        if (!(onFinishLoading is null)) {
            onFinishLoading();
        }
    }

    public T Create<T>(T prototype) where T : IGamePrototype {
        T clone = (T) prototype.Clone();

        // ~ Criar dicionário com eventos de destruição de cada IGamePrototype para remoção
        clone.GameObject = Instantiate(prototype.GameObject);

        return clone;
    }

    public List<T> GetPrototypeList<T>() where T : IGamePrototype {
        return (List<T>) gamePrototypeTypes[typeof(T)];
    }

    // ~ Acrescentar métodos para filtrar/criar entidades, manipular dados da fase etc ~

    public void ClearRuntimeData() {
        
    }

}
