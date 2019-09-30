using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum TileType {START, GOAL, WALKABLE, OBSTACLE, PATH, OPEN_SET, CLOSED_SET};

public class PathFinding : MonoBehaviour {

    private TileType tileType;

    private Vector3Int _startPos;
    public Vector3Int startPos {
        get {
            return _startPos;
        }
        set {
            _startPos = value;
        }
    }

    private Vector3Int _goalPos;
    public Vector3Int goalPos {
        get {
            return _goalPos;
        }
        set {
            _goalPos = value;
        }
    }

    private HashSet<Node> _openSet;
    public HashSet<Node> openSet {
        get {
            return _openSet;
        }
        set {
            _openSet = value;
        }
    }

    private HashSet<Node> _closedSet;
    public HashSet<Node> closedSet {
        get {
            return _closedSet;
        }
        set {
            _closedSet = value;
        }
    }

    private Stack<Vector2> _path;
    public Stack<Vector2> path {
        get {
            return _path;
        }
        set {
            _path = value;
        }
    }

    private Dictionary<Vector3Int, Node> _allNodes;
    public Dictionary<Vector3Int, Node> allNodes {
        get {
            return _allNodes;
        }
        set {
            _allNodes = value;
        }
    }

    [SerializeField]
    private Tilemap _tilemap;
    public Tilemap tilemap { 
        get {
            return _tilemap;
        } 
        set {
            _tilemap = value;
        }
    }

    private Node currentNode;

    private void Start() {
        Tilemap[] tilemaps = new Tilemap[1];
        tilemaps[0] = tilemap;
        allNodes = new NodeTilemap(tilemaps).Nodes;
        openSet = new HashSet<Node>();
        closedSet = new HashSet<Node>();
    }
    // private void Update() {
    //     if(Input.GetMouseButtonDown(0)) {
    //         Vector3Int clickedCell = ScreenToCellPosition(Input.mousePosition);
    //         if(tilemap.HasTile(clickedCell)) {
    //             if(tilemap.GetTile<GameCustomTile>(clickedCell).isWalkable) {
    //                 if(tileType == TileType.START) {
    //                     startPos = clickedCell;
    //                     ++tileType;
    //                 }
    //                 else {
    //                     goalPos = clickedCell;
    //                 }
    //             }
    //         }
    //     }
    //     if(Input.GetKeyDown(KeyCode.Space)) {
    //         FindPath();
    //     }
    // }

    private void Initialize() {
        currentNode = GetNode(startPos);
        openSet.Add(currentNode);
    }

    public void FindPath() {

        if(currentNode == null) {
            Initialize();
        }

        while(openSet.Count > 0 && path == null) {
            List<Node> neighbours = GetNeighbours(currentNode.position);

            ExamineNeighbours(neighbours, currentNode);

            UpdateCurrentTile(ref currentNode);

            path = GeneratePath(currentNode);
        }
    }

    private List<Node> GetNeighbours(Vector3Int nodePos) {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++) {
            for(int y = -1; y <= 1; y++) {
                Vector3Int neighbourPos = new Vector3Int(nodePos.x - x, nodePos.y - y, nodePos.z);
                GameCustomTile neighbourTile = tilemap.GetTile<GameCustomTile>(neighbourPos);
                if(neighbourTile) {
                    if(y != 0 || x != 0) {
                        if(neighbourPos != startPos && neighbourTile.isWalkable) {
                            Node neighbour = GetNode(neighbourPos);
                            neighbours.Add(neighbour);
                        }
                    }
                }
            }
        }

        return neighbours;
    }

    private void ExamineNeighbours(List<Node> neighbours, Node current) {
        for (int i = 0; i < neighbours.Count; i++) {

            Node neighbour = neighbours[i];

            int gScore = DetermineGScore(neighbours[i], current);

            if(openSet.Contains(neighbour)) {
                if(current.gCost + gScore < neighbour.gCost) {
                    CalcValues(current, neighbour, gScore);
                }
            }
            else if(!closedSet.Contains(neighbour)) {
                CalcValues(current, neighbour, gScore);

                openSet.Add(neighbour);
            }

            
        }
    }

    private void CalcValues(Node parent, Node neighbour, int cost) {
        neighbour.parent = parent;

        neighbour.gCost = parent.gCost + cost;

        neighbour.hCost = (Mathf.Abs(neighbour.position.x - goalPos.x) + Mathf.Abs(neighbour.position.y - goalPos.y)) * 10;
    }

    private int DetermineGScore(Node current, Node neighbour) {
        int gCost;
        
        int distX = Mathf.Abs(current.position.x - neighbour.position.x);
        int distY = Mathf.Abs(current.position.y - neighbour.position.y);
        
        if(distX > distY) gCost = 14 * distY + 10 * Mathf.Abs(distX - distY);
        else gCost = 14 * distX + 10 * Mathf.Abs(distX - distY);

        return gCost;
    }

    private void UpdateCurrentTile(ref Node current) {
        openSet.Remove(current);

        closedSet.Add(current);

        if(openSet.Count > 0) {
            current = openSet.OrderBy(x => x.fCost).First();
        }
    }

    private Node GetNode(Vector3Int position) {
        if(allNodes.ContainsKey(position)) {
            return allNodes[position];
        }
        else {
            Node node = new Node(position);
            return node;
        }
    }

    Vector3Int ScreenToCellPosition(Vector2 screenPos) {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
        Vector3Int cellPos = tilemap.WorldToCell(worldPosition);
        cellPos.x -= 1;
        cellPos.y -= 1;
        cellPos.z = 0; 
        return cellPos;
    }

    private Stack<Vector2> GeneratePath(Node current) {
        if(current.position == goalPos) {
            Stack<Vector2> finalPath = new Stack<Vector2>();

            while(current.position != startPos) {
                finalPath.Push(tilemap.CellToWorld(current.position));

                current = current.parent;
            }

            return finalPath;
        }

        return null;
    }

}