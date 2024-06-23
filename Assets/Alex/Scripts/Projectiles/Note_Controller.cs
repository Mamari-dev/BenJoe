using UnityEngine;

public class Note_Controller : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] noteSounds;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable.GetDamage(5.0f);

            audioSource.PlayOneShot(noteSounds[Random.Range(0, noteSounds.Length)]);
        }
    }
}
