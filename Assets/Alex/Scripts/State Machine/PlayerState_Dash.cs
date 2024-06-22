using System.Collections;
using UnityEngine;


public class PlayerState_Dash : PlayerState
{
    [Space(20)]
    [Header("Dash Attributes:")]
    Coroutine dash;

    int currentDashAmount;
    [SerializeField] int dashAmount = 1;
    [SerializeField] float dashSpeed = 16.0f;
    [SerializeField] float dashDuration = 0.2f;

    bool cooldownRunning;
    [SerializeField] float dashCooldown = 1.0f;

    //[Space]
    //[SerializeField] Soundeffect dashSound;
    //[SerializeField] UI_DashPanel dashPanel;

    public override bool IsExecutable()
    {
        if(currentDashAmount > 0)
            return true;
        else
            return false;
    }

    private void Awake()
    {
        currentDashAmount = dashAmount;

        //dashPanel.SetUp(dashAmount);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        dash = StartCoroutine(Dash());
    }

    protected override void OnDisable()
    {
        StopCoroutine(dash);

        player.MovementController.enabled = true;

        base.OnDisable();
    }

    IEnumerator Dash()
    {
        currentDashAmount--;

        //dashPanel.UseDash(currentDashAmount);

        if (!cooldownRunning)
            StartCoroutine(DashCooldown());

        player.MovementController.enabled = false;

        Vector2 dashDirection = player.InputController.AimDirection.normalized;
        //player.AnimationController.SetDirection(dashDirection);
        player.Rigidbody.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);

        //player.Soundboard.PlaySound(dashSound);

        yield return new WaitForSeconds(dashDuration);

        player.MovementController.enabled = true;

        stateMachine.SwitchState(stateMachine.StateMove);
    }

    IEnumerator DashCooldown()
    {
        cooldownRunning = true;

        yield return new WaitForSeconds(dashCooldown);

        currentDashAmount++;

        //dashPanel.GainCharge(currentDashAmount);

        if (currentDashAmount < dashAmount)
            StartCoroutine(DashCooldown());
        else
            cooldownRunning = false;
    }
}
