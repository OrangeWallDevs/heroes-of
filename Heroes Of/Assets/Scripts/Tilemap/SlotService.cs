using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SlotService : MonoBehaviour {
    public TilemapHandler tilemapHandler;
    private const int slotRange = 6;

    public List<Node> GetSlotTilesFromTile(Node slotTile) {
        List<Node> slotTilesNeighbours = tilemapHandler.GetAllTileNeighbours(slotTile, slotRange - 1)
            .Where(node => node.tile.isSlot).ToList();
        
        return slotTilesNeighbours;
    }

    public void BlockSlot(List<Node> slotTiles) {
        foreach(Node slotTile in slotTiles) {
            BlockSlotTile(slotTile);
        }
    }

    public void BlockSlotTile(Node slotTile) {
        slotTile.isAvailable = false;
    }
}
