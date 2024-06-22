using UnityEngine;

public class Note_Movement_Circle : MonoBehaviour
{
    Vector2 center;
    float radius;
    float angle;

    float movingSpeed;

    [Space]
    [SerializeField] Rigidbody2D rb;

    public void Init(Vector2 _center, float _radius, float _angle, float _movingSpeed)
    {
        center = _center;
        angle = _angle;
        radius = _radius;
        movingSpeed = _movingSpeed;

        float x = Mathf.Cos(angle) * radius;  // Berechne die x-Koordinate
        float y = Mathf.Sin(angle) * radius;  // Berechne die y-Koordinate
        rb.MovePosition(center + new Vector2(x, y));  // Setze die Position des Objekts
    }

    void FixedUpdate()
    {
        angle += movingSpeed * Time.fixedDeltaTime;  // Erhöhe den Winkel basierend auf der Zeit und Geschwindigkeit
        float x = Mathf.Cos(angle) * radius;  // Berechne die x-Koordinate
        float y = Mathf.Sin(angle) * radius;  // Berechne die y-Koordinate
        rb.MovePosition(center + new Vector2(x, y));  // Setze die Position des Objekts
    }
}
