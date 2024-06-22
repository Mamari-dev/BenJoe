using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : Pathfinding
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();
    private bool musicIsOn = false;
    [SerializeField] private int minSoundDelay;
    [SerializeField] private int maxSoundDelay;
    private int currentSoundDelay;
    protected virtual void Update()
    {
        if (!musicIsOn)
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (followPlayer)
        {
            if (!audioSource.isPlaying)
            {
                GetRandomSound();
                GetRandomSoundDelay();
                audioSource.Play();
                musicIsOn = true;
                StartCoroutine(SoundCheck());
            }
        }
    }

    private void GetRandomSound()
    {
        int rnd = Random.Range(0, clips.Count);
        AudioClip clip = clips[rnd];
        audioSource.clip = clip;
    }

    private void GetRandomSoundDelay()
    {
        currentSoundDelay = Random.Range(minSoundDelay, maxSoundDelay);
    }

    private IEnumerator SoundCheck()
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);

        yield return new WaitForSeconds(currentSoundDelay);

        musicIsOn = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
