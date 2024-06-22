using UnityEngine;

public class Note_Movement_Snake : MonoBehaviour
{
    float time;
    float lifeTime;

    float amplitude;
    float frequenz;

    Vector2 moveDirection;
    Vector2 normalMoveDirection;
    float movingSpeed;

    [Space]
    [SerializeField] Rigidbody2D rb;

    public void Init(Vector2 _moveDirection, float _movingSpeed, float _amplitude, float _frequenz, float _lifeTime)
    {
        moveDirection = _moveDirection;
        moveDirection.Normalize();
        normalMoveDirection = new Vector2(-moveDirection.y, moveDirection.x);

        movingSpeed = _movingSpeed;
        amplitude = _amplitude;
        frequenz = _frequenz;
        lifeTime = _lifeTime;
    }

    void Update()
    {
        time += Time.deltaTime;
        rb.velocity = (moveDirection * movingSpeed) + (normalMoveDirection * amplitude * Mathf.Sin(time * frequenz));

        if (time > lifeTime)
            Destroy(gameObject);
    }
}
