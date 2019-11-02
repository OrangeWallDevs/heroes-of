using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PathFinding : MonoBehaviour {

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

    private Node currentNode;

    private TilemapHandler tilemapHandler;

    private void Awake() {
        tilemapHandler = GameObject.Find("TilemapHandler").GetComponent<TilemapHandler>();
    }

    private void Initialize() {
        currentNode = tilemapHandler.nodeTilemap.GetNode(startPos);
        openSet = new HashSet<Node>();
        closedSet = new HashSet<Node>();
        openSet.Add(currentNode);
        path = null;
    }

    public void FindPath() {
        Initialize();
    
        while(openSet.Count > 0 && path == null) {
            List<Node> neighbours = currentNode.neighbours;

            ExamineNeighbours(neighbours, currentNode);

            UpdateCurrentTile(ref currentNode);

            path = GeneratePath(currentNode);
        }
    }

    private void ExamineNeighbours(List<Node> neighbours, Node current) {
        for (int i = 0; i < neighbours.Count; i++) {
            
            Node neighbour = neighbours[i];
                
            if(neighbour.position != startPos) {
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


    private Stack<Vector2> GeneratePath(Node current) {
        if(current.position == goalPos) {
            Stack<Vector2> finalPath = new Stack<Vector2>();

            while(current.position != startPos) {
                finalPath.Push(current.tilemapMember.CellToWorld(current.position));

                current = current.parent;
            }

            return finalPath;
        }

        return null;
    }

}