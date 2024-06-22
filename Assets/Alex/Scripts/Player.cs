using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] Player_InputController inputController;
    [SerializeField] Player_MovementController movementController;
    [SerializeField] Player_Dash dashController;

    public Rigidbody2D Rigidbody => rb;

    public Player_InputController InputController => inputController;
    public Player_MovementController MovementController => movementController;
    public Player_Dash DashController => dashController;
}
