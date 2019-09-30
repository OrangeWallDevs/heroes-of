using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public int gCost { get; set; }
    public int hCost { get; set; }
    public int fCost { get { return gCost + hCost; } }
    public Node parent { get; set; }
    public Vector3Int position { get; set; }

    public Node(Vector3Int position) {
        this.position = position;
    }
}
