using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamePrototype {
    
    GameObject GameObject { get; set; }
    bool IsEnemy { get; set; }

}
