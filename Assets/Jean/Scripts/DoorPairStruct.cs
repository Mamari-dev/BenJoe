using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct DoorPairStruct
{
    [SerializeField] private Door door1;
    [SerializeField] private Door door2;

    [SerializeField] private AudioClip pairMelodyClean;
    [SerializeField] private AudioClip pairMelodySlgithtlyDistorted;
    [SerializeField] private AudioClip pairMelodyHighlyDistorted;

    public void OpenPair()
    {
        door1.OpenDoor();
        door2.OpenDoor();
    }

    public AudioClip GetMelodyType(DistortionLevel distortionLevel)
    {
        switch (distortionLevel)
        {
            case DistortionLevel.None:
                return pairMelodyClean;
            case DistortionLevel.Slightly:
                return pairMelodySlgithtlyDistorted;
            case DistortionLevel.Highly:
                return pairMelodyHighlyDistorted;
        }

        throw new NotImplementedException("Couldnt get Melody with Distortion Type");
    }
    
}
