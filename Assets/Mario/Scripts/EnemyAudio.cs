using UnityEngine;

public class EnemyAudio : Pathfinding
{
    [SerializeField] AudioSource audioSource;
    private void Update()
    {
        PlaySound();
    }

    private void PlaySound()
    {
        if (followPlayer && !audioSource.enabled)
        {
            audioSource.enabled = true;
        }
        else if (!followPlayer && audioSource.enabled)
        {
            audioSource.enabled = false;
        }
    }
}
