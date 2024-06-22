using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "AudioSettings", order = 5)]
public class SO_AudioSettings : ScriptableObject
{
    public Sprite ImageAudioOn;
    public Sprite ImageAudioOff;

    public bool MasterOn;
    public List<AudioValues> Values = new List<AudioValues>();
}
