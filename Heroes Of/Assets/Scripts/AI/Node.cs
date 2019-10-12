using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Node {
    public int gCost { get; set; }
    public int hCost { get; set; }
    public int fCost { get { return gCost + hCost; } }
    public bool isAvailable { get; set; }
    public GameCustomTile tile { get; set; }
    public Node parent { get; set; }
    public Vector3Int position { get; set; }
    public Tilemap tilemapMember;
    public List<Node> neighbours;

    public Node(Vector3Int position) {
        this.position = position;
        this.isAvailable = true;
    }
}
