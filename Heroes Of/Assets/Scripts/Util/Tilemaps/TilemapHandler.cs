using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class TilemapHandler : MonoBehaviour {
    public GameRuntimeData gameRuntimeData;

    public Tilemap[] tilemaps;
    private NodeTilemap nodeTilemap;
    private void Start() {
        nodeTilemap = new NodeTilemap(tilemaps);
        //tilemaps = gameRuntimeData.CurrentLevel.Tilemaps;
    }

    public bool IsTile(Vector2 position, ref Tilemap containingTilemap) {
        foreach(Tilemap tilemap in tilemaps) {
            Vector3Int position3Int = ScreenToCellPosition(position, tilemap);
            if(tilemap.HasTile(position3Int)) {
                containingTilemap = tilemap;
                return true;
            }
        }
        return false;
    }

    public Node GetTile(Vector3Int position, Tilemap tilemap) {
        return nodeTilemap.GetNode(position);
        //return gameRuntimeData.NodeTilemap.GetNode(position);
    }

    public Vector3Int ScreenToCellPosition(Vector2 screenPos, Tilemap tilemap) {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
        Vector3Int cellPos = tilemap.WorldToCell(worldPosition);
        cellPos.x -= 1;
        cellPos.y -= 1;
        cellPos.z = 0;
        return cellPos;
    }

    public Node PositionToTilemapNode(Vector2 position) {
        Tilemap tempTilemap = new Tilemap();
        if (IsTile(position, ref tempTilemap)) {
            Vector3Int gridPos = ScreenToCellPosition(position, tempTilemap);
            Node clickedCell = GetTile(gridPos, tempTilemap);

            return clickedCell;
        }

        return null;
    }
} 