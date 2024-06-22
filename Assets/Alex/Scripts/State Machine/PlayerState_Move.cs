using UnityEngine;

public class PlayerState_Move : PlayerState
{
    public override bool IsExecutable() => true;

    protected override void OnEnable()
    {
        base.OnEnable();

        //if (player.InputController.HoldAttack)
        //    stateMachine.SwitchState(stateMachine.StateAttack);
    }

    //void Update() => player.AnimationController.SetDirection(player.InputController.MoveDirection);
}
