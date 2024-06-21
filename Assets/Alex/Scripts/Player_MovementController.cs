using System.Collections;
using UnityEngine;

public class Player_MovementController : MonoBehaviour
{
    [Header("Move Attributes:")]
    EDirection eMoveDirection;
    public EDirection EMoveDirection => eMoveDirection;

    [SerializeField] float movementSpeed = 1.0f;
    float currentMovementSpeed;
    Vector2 movementDirection;


    [Space]
    [SerializeField] Rigidbody2D rb;
    [Space]
    [SerializeField] Player_InputController characterInputController;


    void Awake() => currentMovementSpeed = movementSpeed;

    void Update() => movementDirection = characterInputController.MoveDirection;

    void FixedUpdate() => rb.velocity = movementDirection * currentMovementSpeed;

    public void ChangeSpeed(float _speedMultiplicator) => currentMovementSpeed = movementSpeed * _speedMultiplicator;

    public void ChangeSpeed(float _speedMultiplicator, float _duration)
    {
        currentMovementSpeed = movementSpeed * _speedMultiplicator;

        StopAllCoroutines();
        StartCoroutine(ResetSpeed(_duration));
    }

    IEnumerator ResetSpeed(float _duration)
    {
        yield return new WaitForSeconds(_duration);
        currentMovementSpeed = movementSpeed;
    }
}
