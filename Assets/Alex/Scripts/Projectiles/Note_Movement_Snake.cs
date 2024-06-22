using UnityEngine;

public class Note_Movement_Snake : MonoBehaviour
{
    float time;
    [SerializeField] float amplitude;
    [SerializeField] float frequenz;

    [SerializeField] Vector2 moveDirection;
    Vector2 normalMoveDirection;
    [SerializeField] float movingSpeed;

    [Space]
    [SerializeField] Rigidbody2D rb;

    private void Awake()
    {
        moveDirection.Normalize();
        normalMoveDirection = new Vector2(-moveDirection.y, moveDirection.x);
    }

    void Update()
    {
        time += Time.deltaTime;
        rb.velocity = (moveDirection * movingSpeed) + (normalMoveDirection * amplitude * Mathf.Sin(time * frequenz));
    }
}
