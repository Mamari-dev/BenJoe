using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PanoramaStruct
{
    public PairID pairID;
    public PanoramaPart part;

    public Sprite panoramaPartSprite;
}

[CreateAssetMenu(fileName = "PanoramaStructsSO", menuName = "Panorama Structs")]
public class PanoramaSO : ScriptableObject
{
    public List<PanoramaStruct> panoramaStructs;
}
