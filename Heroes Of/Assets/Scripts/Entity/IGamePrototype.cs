using System;
using UnityEngine;

public interface IGamePrototype : ICloneable {
    
    GameObject GameObject { get; set; }
    bool IsEnemy { get; set; }

}
