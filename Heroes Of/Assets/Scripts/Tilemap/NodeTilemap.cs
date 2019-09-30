using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeTilemap {

    public Dictionary<Vector3Int, Node> Nodes {
        get; private set;
    }

    public NodeTilemap(Tilemap[] tilemaps) {
        Nodes = new Dictionary<Vector3Int, Node>();

        foreach (Tilemap tilemap in tilemaps) {
            SetupNodeDictionary(tilemap);
        }      
    }

    void SetupNodeDictionary(Tilemap tilemap) {
        BoundsInt bounds = tilemap.cellBounds;
        Vector3Int tilePosition;
        GameCustomTile tile;
        // foreach (TileBase tileBase in tilemap.GetTilesBlock(bounds)) {
        //     if(tileBase != null) {
        //         tile = (GameCustomTile) tileBase;
        //         tilePosition = tilemap.WorldToCell(tile.gameObject.transform.position);
        //         Debug.Log(tile);
        //     }
        // }
        // // GameCustomTile tile;
        
        for (int x = bounds.xMin; x <= bounds.xMax; x++) {
            for (int y = bounds.yMin; y <= bounds.yMax; y++) {
                tilePosition = new Vector3Int(x, y, 0);
                tile = tilemap.GetTile<GameCustomTile>(tilePosition);
                if (tile) {
                    Nodes.Add(tilePosition, new Node(tilePosition));
                    // tile.gridPos = tilePosition;
                    // tile.tilemapMember = tilemap;
                    // Debug.Log(tile.gridPos);
                }
            }
        }
    }
}
