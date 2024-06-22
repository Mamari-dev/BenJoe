using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    [SerializeField] bool isInterruptable;

    //[SerializeField] bool hasSpriteSheet;
    //[SerializeField] Texture stateSpriteSheet;
    //[SerializeField] string animatorBool;

    public bool IsInterruptable => isInterruptable;

    public void Init(PlayerStateMachine _stateMachine, Player _player)
    {
        stateMachine = _stateMachine;
        player = _player;
    }

    public abstract bool IsExecutable();

    protected virtual void OnEnable()
    {
        //should be called first in every State
        //if (hasSpriteSheet)
        //{
        //    player.AnimationController.Animator.SetBool(animatorBool, true);
        //    player.TextureReference.ChangeTexture(stateSpriteSheet);
        //}
        //else
        //    player.TextureReference.ChangeTextureDefault();


    }

    protected virtual void OnDisable()
    {
        //Should be called last in every State
        //if(hasSpriteSheet)
        //    player.AnimationController.Animator.SetBool(animatorBool, false);
    }
}
