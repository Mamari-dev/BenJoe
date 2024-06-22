using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[Serializable]
public enum AudioTypes
{
    Master = 0,
    Melody,
    Sfx,
    Environment,
    Adlibs
}

[Serializable]
public class AudioSettings : MonoBehaviour
{
    [SerializeField] private SO_AudioSettings so_Settings;
    private AudioUI uiScript;

    [SerializeField] private AudioTypes audioType;
    [Space]
    [SerializeField] private AudioMixerGroup mixerGroup;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Slider slider;
    [SerializeField] private string volumeName;

    private int listIdx;
    private bool audioOn;

    #region SaveSystem

    public void LoadSettings(SO_AudioSettings _settings, AudioUI _uiScript)
    {
        uiScript = _uiScript;
        so_Settings = _settings;

        for (int i = 0; i < so_Settings.Values.Count; i++)
        {
            if (so_Settings.Values[i].audioType == audioType)
            {
            Debug.Log(so_Settings.Values[i].audioType);
                listIdx = i;

                AudioValues val = so_Settings.Values[i];
                audioOn = val.audioOn;
                slider.value = val.volume;

                if (so_Settings.MasterOn) ToggleAudio(audioOn);
                else ToggleAudio(false);
            }
        }
    }

    public void OnDisable()
    {
        AudioValues val = so_Settings.Values[listIdx];

        val.audioOn = audioOn;
        val.volume = slider.value;

        so_Settings.Values[listIdx] = val;
    }

    #endregion

    #region Input Handling

    public void OnSetVolume()
    {
        mixerGroup.audioMixer.SetFloat(volumeName, slider.value);

        // save change in the SO
        AudioValues val = so_Settings.Values[listIdx];
        val.volume = slider.value;
        so_Settings.Values[listIdx] = val;
    }

    public void OnToggleAudio(bool _enable)
    {
        audioOn = _enable;

        // save change in the SO
        AudioValues val = so_Settings.Values[listIdx];
        val.audioOn = audioOn;
        so_Settings.Values[listIdx] = val;

        ToggleAudio(audioOn);

        if (audioType == AudioTypes.Master)
            uiScript.ChangeMaster(audioOn);
    }

    /// <summary>
    /// Dis-/Enables the AudioSource depending on the master
    /// </summary>
    /// <param name="_enabled">Is the Master enabled?</param>
    public void InfluenceToggle(bool _enabled)
    {
        if (audioType == AudioTypes.Master) return;

        if (_enabled)
            ToggleAudio(audioOn);
        else ToggleAudio(false);
    }

    private void ToggleAudio(bool _toggle)
    {
        slider.interactable = _toggle;
        toggle.isOn = _toggle;

        if (_toggle)
            toggle.gameObject.GetComponent<Image>().sprite = so_Settings.ImageAudioOn;
        else
            toggle.gameObject.GetComponent<Image>().sprite = so_Settings.ImageAudioOff;

        if (_toggle)
            mixerGroup.audioMixer.SetFloat(volumeName, so_Settings.Values[listIdx].volume);
        else
            mixerGroup.audioMixer.SetFloat(volumeName, -80f);
    }

    #endregion
}

[Serializable]
public struct AudioValues
{
    public AudioTypes audioType;
    [Space]
    [Tooltip("Is the Audio Source enabled?")]
    public bool audioOn;
    public float volume;
}
