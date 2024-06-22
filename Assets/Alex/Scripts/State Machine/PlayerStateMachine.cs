using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] Player player;

    PlayerState currentState;
    [Space(20)]
    [SerializeField] PlayerState_Move stateMove;
    [SerializeField] PlayerState_Dash stateDash;



    public PlayerState_Move StateMove => stateMove;
    public PlayerState_Dash StateDash => stateDash;


    private void Awake()
    {
        stateMove.Init(this, player);
        stateDash.Init(this, player);
    }

    private void Start()
    {
        currentState = stateMove;
        currentState.enabled = true;
    }

    public void SwitchState(PlayerState _targetState)
    {
        if (_targetState == null || currentState == _targetState ||
           (!currentState.IsInterruptable && _targetState != stateMove) ||
           !_targetState.IsExecutable())
            return;

        currentState.enabled = false;

        currentState = _targetState;

        currentState.enabled = true;
    }
}
