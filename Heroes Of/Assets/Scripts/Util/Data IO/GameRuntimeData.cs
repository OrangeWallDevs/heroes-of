using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class GameRuntimeData : ScriptableObject {

    // Runtime members:
    public Phase CurrentLevel {
        get; private set;
    }

    public NodeTilemap NodeTilemap {
        get; private set;
    }

    // Methods:

    void OnEnable() {
        // Carrega o tilemap da fase:
        // Grid levelGrid = Instantiate(dataUtil
        //         .LoadAsset<Grid>("test", new[] {"Assets/Prefabs"}));

        // CurrentLevel = new Phase(levelGrid);
        // NodeTilemap = new NodeTilemap(CurrentLevel.Tilemaps);

        // Debug.Log(NodeTilemap.Nodes.Count);
    }

    public void Load(GamePrimaryData primaryData) {

    }

}
