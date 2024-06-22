using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player_Dash : MonoBehaviour
{
    [Header("Dash Attributes:")]
    bool isDashing;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;

    [Space]
    int currentDashAmount;
    [SerializeField] int dashAmount;

    bool cooldownIsRunning;
    [SerializeField] float dashCooldown;

    [Space]
    [Header("References:")]
    [SerializeField] Player player;


    public bool IsDashing => isDashing;


    private void Awake() => currentDashAmount = dashAmount;

    public void Dash()
    {
        if (!isDashing && !cooldownIsRunning)
            StartCoroutine(StartDash());
        else
            return;
    }


    public IEnumerator StartDash()
    {
        if(isDashing || currentDashAmount == 0)
        {
            Debug.Log("Break");
            yield break;
        }

        isDashing = true;

        currentDashAmount--;

        player.MovementController.enabled = false;
        player.Rigidbody.AddForce(player.InputController.MoveDirection.normalized * dashSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        player.Rigidbody.velocity = Vector2.zero;
        player.MovementController.enabled = true;
        isDashing = false;

        if(!cooldownIsRunning)
            StartCoroutine(DashCooldown());
    }

    public IEnumerator DashCooldown()
    {
        cooldownIsRunning = true;

        yield return new WaitForSeconds(dashCooldown);

        currentDashAmount++;

        if (currentDashAmount < dashAmount)
            StartCoroutine(DashCooldown());
        else
            cooldownIsRunning = false;
    }
}
