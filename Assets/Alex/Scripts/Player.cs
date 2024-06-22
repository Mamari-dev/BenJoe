using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] Player_InputController inputController;
    [SerializeField] Character_AnimationController animationController;

    [SerializeField] Player_MovementController movementController;
    [SerializeField] Player_Dash dashController;

    [SerializeField] Transform enemyContainer;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] AudioSource audioSource;


    public Player_InputController InputController => inputController;
    public Character_AnimationController AnimationController => animationController;

    public Player_MovementController MovementController => movementController;
    public Player_Dash DashController => dashController;

    public Rigidbody2D Rigidbody => rb;
    public AudioSource AudioSource => audioSource;

    public void GetDamage(float value)
    {
        GameManager.Instance.Ouch(value);
    }

    public void GetEnemy(Transform enemyTransform)
    {
        Debug.Log("GetEnemy");
        enemyTransform.SetParent(enemyContainer);
    }
}
