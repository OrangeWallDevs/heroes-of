using UnityEngine;
using System;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public abstract class Entity {
    
    [NonSerialized] GameObject _gameObject;

    public GameObject GameObject {
        get {
            return _gameObject;
        }
        protected set {
            _gameObject = value;
        }
    }

    public Entity(GameObject gameObject) {
        GameObject = gameObject;
    }
    
}
