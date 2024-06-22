using System.Collections;
using UnityEngine;

public class Player_MovementController : MonoBehaviour, ISlowable
{
    [Header("Move Attributes:")]
    EDirection eMoveDirection;
    public EDirection EMoveDirection => eMoveDirection;

    [SerializeField] float movementSpeed = 1.0f;
    float currentMovementSpeed;
    Vector2 movementDirection;


    [Space]
    [SerializeField] Player player;


    void Awake() => currentMovementSpeed = movementSpeed;

    void OnEnable() => player.AudioSource.Play();

    void Update()
    {
        if(!player.DashController.IsDashing)
        {
            movementDirection = player.InputController.MoveDirection;

            if (movementDirection == Vector2.zero && player.AudioSource.isPlaying)
                player.AudioSource.Pause();
            else if(movementDirection != Vector2.zero && !player.AudioSource.isPlaying)
                player.AudioSource.Play();

            player.AnimationController.SetDirection(movementDirection);
        }

    }

    void FixedUpdate() => player.Rigidbody.velocity = movementDirection * currentMovementSpeed;

    void OnDisable() => player.AudioSource.Pause();

    public void ChangeSpeed(float _speedMultiplicator) => currentMovementSpeed = movementSpeed * _speedMultiplicator;
    public void ResetSpeed() => currentMovementSpeed = movementSpeed;
}
