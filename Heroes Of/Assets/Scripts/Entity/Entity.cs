using UnityEngine;
using System;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.Fields)]
public abstract class Entity : ICloneable {
    
    public Entity() {
    }
    
    public virtual object Clone() {
        return MemberwiseClone();
    }

}
