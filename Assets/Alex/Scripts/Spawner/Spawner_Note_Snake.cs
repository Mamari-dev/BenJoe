using UnityEngine;

public class Spawner_Note_Snake : MonoBehaviour
{
    [Header("Spawner Attributes")]
    [SerializeField] Vector2 spawnDirection;
    float timer;
    [SerializeField] float spawnFrequenz;

    [Space]
    [Header("Note Attributes")]
    [SerializeField] float noteMovingSpeed;
    [SerializeField] float noteAmplitude;
    [SerializeField] float noteFrequenz;
    [SerializeField] float noteLifeTime;

    [SerializeField] GameObject notePrefab;

    private void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > spawnFrequenz)
        {
            Note_Movement_Snake note = Instantiate(notePrefab, transform.position, Quaternion.identity).GetComponent<Note_Movement_Snake>();
            note.Init(spawnDirection, noteMovingSpeed, noteAmplitude, noteFrequenz, noteLifeTime);

            timer = 0.0f;
        }
    }
}
