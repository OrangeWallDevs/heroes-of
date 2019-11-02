using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TilemapHandler : MonoBehaviour {
    public GameRuntimeData gameRuntimeData;

    public Tilemap tilemap;
    public NodeTilemap nodeTilemap {get; private set;}
    private void Start() {
        nodeTilemap = new NodeTilemap(tilemap);
        //tilemaps = gameRuntimeData.CurrentLevel.Tilemaps;
    }

    public bool IsTile(Vector2 position) {
        return tilemap.HasTile(WorldToCellPosition(position));
    }

    public Node GetTile(Vector3Int position) {
        return nodeTilemap.GetNode(position);
        //return gameRuntimeData.NodeTilemap.GetNode(position);
    }

    public Vector3Int ScreenToCellPosition(Vector2 screenPos) {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
        Vector3Int cellPos = tilemap.WorldToCell(worldPosition);
        // cellPos.x -= 1;
        // cellPos.y -= 1;
        cellPos.z = 0;
        return cellPos;
    }

    public Node ScreenPositionToTilemapNode(Vector2 position) {
        if (IsTile(Camera.main.ScreenToWorldPoint(position))) {
            Vector3Int gridPos = ScreenToCellPosition(position);
            Node clickedCell = GetTile(gridPos);

            return clickedCell;
        }

        return null;
    }

    public Node PositionToTilemapNode(Vector2 position) {
        Vector3Int gridPos = tilemap.WorldToCell(position);
        if (tilemap.HasTile(gridPos)) {
            return GetTile(gridPos);
        }

        return null;
    }

    public Vector3Int WorldToCellPosition(Vector2 worldPos) {
        return tilemap.WorldToCell(worldPos);
    }

    public List<Node> GetAllTileNeighbours(Node node, int range) {
        return nodeTilemap.GetNeighbours(node, range);
    }
} 