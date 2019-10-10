using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvaliableTileSelection : MonoBehaviour {


    public static void ChangeMask(TilemapHandler tilemapHandler, bool putMask){

		foreach(KeyValuePair<Vector3Int, Node> pair in tilemapHandler.nodeTilemap.Nodes) {
			if( pair.Value.tile.isSlot && pair.Value.tile.isSlotAvailable && putMask){
				pair.Value.tile.color = new Color(0f,56f,251f,1f);

			}else if( pair.Value.tile.isSlot && pair.Value.tile.isSlotAvailable && !putMask){
				pair.Value.tile.color = new Color(255f,255f,255f,255f);
			}
		}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }
}
