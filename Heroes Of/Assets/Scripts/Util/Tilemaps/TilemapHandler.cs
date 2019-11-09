using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System.Linq;


public class TilemapHandler : MonoBehaviour {
    public GameRuntimeData gameRuntimeData;

    public Tilemap tilemap;
    public NodeTilemap nodeTilemap {get; private set;}
    private void Start() {
        nodeTilemap = new NodeTilemap(tilemap);
    }

    public bool IsTile(Vector2 position) {
        return tilemap.HasTile(WorldToCellPosition(position));
    }

    public Node GetTile(Vector3Int position) {
        return nodeTilemap.GetNode(position);
    }

    public Vector3Int ScreenToCellPosition(Vector2 screenPos) {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
        Vector3Int cellPos = tilemap.WorldToCell(worldPosition);
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

    public Vector2 GetClosestWalkableTile(Vector2 current) {
        Vector3Int currentPosition = WorldToCellPosition(current);
        Node currentNode = nodeTilemap.GetNode(currentPosition);

        List<Node> neighbours = GetAllTileNeighbours(currentNode, 6);
        neighbours = neighbours.Where(neighbour => neighbour.tile.isWalkable).ToList();
        
        Vector3Int closestWalkableNodePosition = neighbours[1].position;

        return tilemap.CellToWorld(closestWalkableNodePosition);
    }
} 