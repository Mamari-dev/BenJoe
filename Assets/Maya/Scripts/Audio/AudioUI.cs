using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUI : MonoBehaviour
{
    [SerializeField] private SO_AudioSettings so_Settings;
    [SerializeField] private AudioSettings[] settings;

    private void Start()
    {
        for(int i = 0; i < settings.Length; i++)
        {
            settings[i].LoadSettings(so_Settings, this);
        }
    }

    /// <summary>
    /// When the Master get changed, dis-/enable all other audio sources
    /// </summary>
    /// <param name="_enabled"></param>
    public void ChangeMaster(bool _enabled)
    {
        so_Settings.MasterOn = _enabled;

        foreach (AudioSettings setting in settings)
        {
            setting.InfluenceToggle(_enabled);
        }
    }
}
