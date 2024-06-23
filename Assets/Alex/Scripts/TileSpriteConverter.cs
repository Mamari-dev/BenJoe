using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSpriteConverter : MonoBehaviour
{
    [SerializeField] Tilemap tilemapToRead;
    [SerializeField] Sprite[] tileSpritesToConvert;

    [SerializeField] GameObject[] prefabToSpawn;
    [SerializeField] Transform colliderContainer;

    // Start is called before the first frame update
    void Start()
    {
        for(int y = -200; y < 200; y++)
        {
            for (int x = -200; x < 200; x++)
            {
                Sprite currentSprite = tilemapToRead.GetSprite(new Vector3Int(x, y, 0));

                if(currentSprite != null)
                {
                    int indexOfSprite = Array.IndexOf(tileSpritesToConvert, currentSprite);

                    if(indexOfSprite > 0)
                    {
                        Debug.Log(new Vector3Int(x, y, 0) + " " + new Vector3(x, y, 0)); 
                        
                        Instantiate(prefabToSpawn[indexOfSprite - 1], tilemapToRead.CellToWorld(new Vector3Int(x, y, 0)), Quaternion.identity, colliderContainer);
                    }
                }
            }
        }
    }
}
