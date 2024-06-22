using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct AudioClips
{
    public string name;
    public List<AudioClip> audioClip;
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioClips> audioListen;

    [SerializeField] private AudioSource audioSourceMelody;
    [SerializeField] private AudioSource audioSourceMelodySlightly;
    [SerializeField] private AudioSource audioSourceMelodyHighly;

    [SerializeField] private AudioSource audioSourceEnvironment;
    [SerializeField] private AudioSource audioSourceAdLibs;
    [SerializeField] private AudioSource audioSourceWinLoose;

    private bool isIdeling;

    [SerializeField] private float fadeMultiplier = 0.1f;
    private bool fadeInNone = false, fadeInSlightly = false, fadeInHighly = false;

    [SerializeField] private int adlibDelayMin, adlibDelayMax;
     private bool isAdlibDelayed;

    private List<DoorPairStruct> doorPairs;
    public List<DoorPairStruct> DoorPairs {set => doorPairs = value; }

    private void Update()
    {
        if(isIdeling)
        {
            CheckIsPlaying();
        }
        else
        {
            if (fadeInNone)
            {
                FadeInNone();
            }
            else if (fadeInSlightly)
            {
                FadeInSlightly();
            }
            else if (fadeInHighly)
            {
                FadeInHighly();
            }
        }  
    }

    public void ChangeMelodyDistortion(DistortionLevel distortionLevel)
    {
        SetAllFadesFalse();

        switch (distortionLevel)
        {
            case DistortionLevel.None:
                fadeInNone = true;
                break;
            case DistortionLevel.Slightly:
                fadeInSlightly = true;
                break;
            case DistortionLevel.Highly:
                fadeInHighly = true;
                break;
        }
    }

    private void FadeInNone()
    {
        if(audioSourceMelody.volume <= 1)
        {
            audioSourceMelody.volume += Time.deltaTime * fadeMultiplier;
        }
        else
        {
            audioSourceMelody.volume = 1;
            SetAllFadesFalse();
        }
    }

    private void FadeInSlightly()
    {
        if (audioSourceMelodySlightly.volume <= 1)
        {
            audioSourceMelody.volume -= Time.deltaTime * fadeMultiplier;
            audioSourceMelodySlightly.volume += Time.deltaTime * fadeMultiplier;

        }
        else
        {
            audioSourceMelody.volume = 0;
            audioSourceMelodySlightly.volume = 1;
            SetAllFadesFalse();
        }
    }

    private void FadeInHighly()
    {
        if (audioSourceMelodyHighly.volume <= 1)
        {
            audioSourceMelodySlightly.volume -= Time.deltaTime * fadeMultiplier;
            audioSourceMelodyHighly.volume += Time.deltaTime * fadeMultiplier;

        }
        else
        {
            audioSourceMelodySlightly.volume = 0;
            audioSourceMelodyHighly.volume = 1;
            SetAllFadesFalse();
        }
    }

    private void SetAllFadesFalse()
    {
        fadeInNone = false;
        fadeInSlightly = false;
        fadeInHighly = false;
    }

    public void StartMelodyID(PairID pair)
    {
        audioSourceMelody.clip = doorPairs[(int)pair - 1].GetMelodyType(DistortionLevel.None);
        audioSourceMelody.volume = 0;
        audioSourceMelody.Play();

        audioSourceMelodySlightly.clip = doorPairs[(int)pair - 1].GetMelodyType(DistortionLevel.Slightly);
        audioSourceMelodySlightly.volume = 0;
        audioSourceMelodySlightly.Play();

        audioSourceMelodyHighly.clip = doorPairs[(int)pair - 1].GetMelodyType(DistortionLevel.Highly);
        audioSourceMelodyHighly.volume = 0;
        audioSourceMelodyHighly.Play();

        fadeInNone = true;
    }

    public void StopMelodySounds()
    {
        audioSourceMelody.Stop();
        audioSourceMelodySlightly.Stop();
        audioSourceMelodyHighly.Stop(); 
    }

    public void StartIdleSounds()
    {
        audioSourceEnvironment.clip = GetRandomSound("Environment");
        audioSourceEnvironment.Play();

        audioSourceAdLibs.clip = GetRandomSound("Environment");
        audioSourceAdLibs.Play();

        isIdeling = true;
    }

    public void StopIdleSounds()
    {
        audioSourceEnvironment.Stop();
        audioSourceAdLibs.Stop();

        isIdeling = true;
    }

    public void PlayCorrectSound()
    {
        audioSourceWinLoose.clip = GetRandomSound("Win");
        audioSourceWinLoose.Play();
    }

    public void PlayIncorrectSound()
    {
        audioSourceWinLoose.clip = GetRandomSound("Loose");
        audioSourceWinLoose.Play();
    }

    private AudioClip GetRandomSound(string soundName)
    {
        foreach (AudioClips audioClips in audioListen)
        {
            if (audioClips.name == soundName)
            {
                int randomSound = UnityEngine.Random.Range(0, audioClips.audioClip.Count);
                return audioClips.audioClip[randomSound];
            }
        }

        throw new NotImplementedException("Sound Name wurde nicht gefunden!!!");
    }

    private void CheckIsPlaying()
    {
        if(!audioSourceEnvironment.isPlaying)
        {
            audioSourceEnvironment.clip = GetRandomSound("Environment");
            audioSourceEnvironment.Play();
        }
        if (!audioSourceAdLibs.isPlaying && !isAdlibDelayed)
        {
            isAdlibDelayed = true;
            StartCoroutine(StartAdlibDelay());
        }
    }

    IEnumerator StartAdlibDelay()
    {
        int randomDelay = UnityEngine.Random.Range(adlibDelayMin, adlibDelayMax);

        yield return new WaitForSeconds(randomDelay);

        audioSourceAdLibs.clip = GetRandomSound("Adlibs");
        audioSourceAdLibs.Play();
        isAdlibDelayed = false;
    }
}
