using UnityEngine;

public class Note_Movement_Circle : MonoBehaviour
{
    float angle;
    [SerializeField] float startAngle;

    [SerializeField] float radius;
    [SerializeField] float movingSpeed;


    [Space]
    [SerializeField] Rigidbody2D rb;

    private void Awake()
    {
        float x = Mathf.Cos(angle) * radius;  // Berechne die x-Koordinate
        float y = Mathf.Sin(angle) * radius;  // Berechne die y-Koordinate
        rb.position = new Vector3(x, y, 0);  // Setze die Position des Objekts
    }

    void FixedUpdate()
    {
        float angle = 0.0f;
        angle += movingSpeed * Time.fixedDeltaTime;  // Erhöhe den Winkel basierend auf der Zeit und Geschwindigkeit
        float x = Mathf.Cos(angle) * radius;  // Berechne die x-Koordinate
        float y = Mathf.Sin(angle) * radius;  // Berechne die y-Koordinate
        rb.position = new Vector3(x, y, 0);  // Setze die Position des Objekts
    }
}
