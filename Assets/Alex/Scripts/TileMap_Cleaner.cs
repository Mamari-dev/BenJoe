using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMap_Cleaner : MonoBehaviour
{
    [SerializeField] Tilemap tilemapToClean;
    [SerializeField] TileBase[] allowedTiles;

    // Start is called before the first frame update
    void Start()
    {
        for (int y = -200; y < 200; y++)
        {
            for (int x = -200; x < 200; x++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                TileBase tile = tilemapToClean.GetTile(cellPosition);

                if (tile && !allowedTiles.Contains(tile))
                    tilemapToClean.SetTile(cellPosition, null);                    
            }
        }
    }
}
