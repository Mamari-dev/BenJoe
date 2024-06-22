using UnityEngine;

public class Spawner_Note_Circle : MonoBehaviour
{
    [Header("Spawner Attributes")]
    [SerializeField, Range(1, 20)] int spawnAmount;

    [Space]
    [Header("Note Attributes")]
    [SerializeField] float noteRadius;
    [SerializeField] float noteMovingSpeed;

    [SerializeField] GameObject notePrefab;

    private void Start()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            Note_Movement_Circle note = Instantiate(notePrefab, transform.position, Quaternion.identity).GetComponent<Note_Movement_Circle>();

            float spawnAngle = i * (2 * Mathf.PI / spawnAmount);

            note.Init(transform.position ,noteRadius, spawnAngle, noteMovingSpeed);
        }
    }
}
