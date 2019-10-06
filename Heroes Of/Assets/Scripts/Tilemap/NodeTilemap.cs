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
        
        for (int x = bounds.xMin; x <= bounds.xMax; x++) {
            for (int y = bounds.yMin; y <= bounds.yMax; y++) {
                tilePosition = new Vector3Int(x, y, 0);
                tile = tilemap.GetTile<GameCustomTile>(tilePosition);
                if (tile) {
                    Node node = new Node(tilePosition);
                    node.tilemapMember = tilemap;
                    node.tile = tile;
                    Nodes.Add(tilePosition, node);
                }
            }
        }

        foreach(KeyValuePair<Vector3Int, Node> pair in Nodes) {
            pair.Value.neighbours = GetNeighbours(pair.Value);
        }
        
    }

    public Node GetNode(Vector3Int position) {
        if(Nodes.ContainsKey(position)) {
            return Nodes[position];
        }
        else {
            Node node = new Node(position);
            Nodes.Add(position, node);
            return node;
        }
    }

    private List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++) {
            for(int y = -1; y <= 1; y++) {
                Vector3Int neighbourPos = new Vector3Int(node.position.x - x, node.position.y - y, node.position.z);
                GameCustomTile neighbourTile = node.tilemapMember.GetTile<GameCustomTile>(neighbourPos);
                if(neighbourTile) {
                    if(y != 0 || x != 0) {
                        if(neighbourTile.isWalkable) {
                            Node neighbour = GetNode(neighbourPos);
                            neighbours.Add(neighbour);
                        }
                    }
                }
            }
        }

        return neighbours;
    }
}
