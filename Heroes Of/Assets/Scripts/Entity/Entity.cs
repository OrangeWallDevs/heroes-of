using UnityEngine;
using System;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public abstract class Entity {
    
    public Entity() {
    }
    
}
