using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSpriteConverter : MonoBehaviour
{
    [SerializeField] Tilemap tilemapToRead;
    [SerializeField] Sprite[] tileSpritesToConvert;

    [Space]
    [SerializeField] float spawnOffsetY;
    [SerializeField] Tilemap tilemapObstacles;

    [Space]
    [SerializeField] GameObject[] prefabToSpawn;
    [SerializeField] Transform colliderContainer;

    // Start is called before the first frame update
    void Start()
    {
        for(int y = -200; y < 200; y++)
        {
            for (int x = -200; x < 200; x++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);

                //Canyon underneath
                Sprite currentSprite = tilemapToRead.GetSprite(cellPosition);
                if(currentSprite != null)
                {
                    int indexOfSprite = Array.IndexOf(tileSpritesToConvert, currentSprite);

                    //CanyonHeight
                    if(indexOfSprite >= 0)
                    {
                        //Just Edges
                        if(indexOfSprite > 0)
                        {
                            Instantiate(prefabToSpawn[indexOfSprite], tilemapToRead.CellToWorld(cellPosition) + new Vector3(0, spawnOffsetY, 0), Quaternion.identity, colliderContainer);
                        }


                        TileBase obstacle = tilemapObstacles.GetTile(cellPosition);

                        if(obstacle != null)
                            Instantiate(prefabToSpawn[indexOfSprite], tilemapToRead.CellToWorld(cellPosition) + new Vector3(0, spawnOffsetY - 1.0f, 0), Quaternion.identity, colliderContainer);                        
                    }
                }
            }
        }
    }
}
