using UnityEngine;
//using UnityEngine.InputSystem;

public class Player_InputController : MonoBehaviour
{
    bool isUsingGamepad;

    Vector2 moveDirection;

    bool isActivelyAiming;
    Vector2 aimDirection;

    bool holdAttack;

    public Vector2 MoveDirection => moveDirection;
    public Vector2 AimDirection => aimDirection;
    public bool HoldAttack => holdAttack;


    //public void OnControlsChanged(PlayerInput _input)
    //{
    //    isUsingGamepad = _input.currentControlScheme == "Gamepad";
    //
    //    Cursor.visible = !isUsingGamepad;
    //    Cursor.lockState = isUsingGamepad ? CursorLockMode.Locked : CursorLockMode.None;
    //}
    //
    //public void OnMove(InputAction.CallbackContext _inputPhase)
    //{
    //    moveDirection = _inputPhase.ReadValue<Vector2>();
    //
    //    if(isUsingGamepad && !isActivelyAiming)
    //        aimDirection = _inputPhase.ReadValue<Vector2>();
    //}
    //
    //public void OnAim(InputAction.CallbackContext _inputPhase)
    //{
    //    if (isUsingGamepad)
    //    {
    //        isActivelyAiming = true;
    //        aimDirection = _inputPhase.ReadValue<Vector2>();
    //    }
    //    else
    //        aimDirection = Camera.main.ScreenToWorldPoint(_inputPhase.ReadValue<Vector2>()) - transform.position;
    //
    //    if(_inputPhase.canceled && isUsingGamepad)
    //        isActivelyAiming = false;
    //}
    //
    //public void OnDash(InputAction.CallbackContext _inputPhase)
    //{
    //    if (_inputPhase.performed)
    //        player.StateMachine.SwitchState(player.StateMachine.StateDash);
    //}
    //
    //public void OnAttack(InputAction.CallbackContext _inputPhase)
    //{
    //    if (_inputPhase.performed)
    //    {
    //        holdAttack = true;
    //        player.StateMachine.SwitchState(player.StateMachine.StateAttack);
    //    }
    //    else if (_inputPhase.canceled)
    //        holdAttack = false;
    //}
    //
    //public void OnPower(InputAction.CallbackContext _inputPhase)
    //{
    //    if (_inputPhase.performed)
    //        player.StateMachine.SwitchState(player.StateMachine.StatePower);
    //}
    //
    //public void OnSpecial(InputAction.CallbackContext _inputPhase)
    //{
    //    if (_inputPhase.performed)
    //        player.StateMachine.SwitchState(player.StateMachine.StateSpecial);
    //}
    //
    //public void OnDefence(InputAction.CallbackContext _inputPhase)
    //{
    //    if (_inputPhase.performed)
    //        player.StateMachine.SwitchState(player.StateMachine.StateDefence);
    //}
    //
    //public void OnUltimate(InputAction.CallbackContext _inputPhase)
    //{
    //    if (_inputPhase.performed)
    //        player.StateMachine.SwitchState(player.StateMachine.StateUltimate);
    //}
}
