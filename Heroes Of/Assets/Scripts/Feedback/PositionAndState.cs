using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PositionAndState {
    private Vector2 position;
    private bool isAtPosition;

    public PositionAndState(Vector2 position, bool isAtPosition){
        this.position = position;
        this.isAtPosition = isAtPosition;
    }

    public bool IsAtPosition { get => isAtPosition; set => isAtPosition = value; }
    public Vector2 Position { get => position; set => position = value; }
}
