using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OpenCreditScene();
        }
    }

    private void OpenCreditScene()
    {
        // Credits müssen sich öffnen
        UIManager.Instance.Endscreen();
        audioSource.Play(); 
    }
}
