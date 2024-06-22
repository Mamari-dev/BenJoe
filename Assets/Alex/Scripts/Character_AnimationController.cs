using UnityEngine;


public class Character_AnimationController : MonoBehaviour
{
    EDirection eAnimationDirection;
    bool turnedUp;
    bool turnedLeft;

    [SerializeField] Animator animator;
    [SerializeField] Transform spriteTransform;

    public Animator Animator => animator;


    public void SetDirection(Vector2 _animationDirection)
    {
        if(_animationDirection != Vector2.zero)
        {
            if (!turnedLeft && _animationDirection.x < 0.0f)
                turnedLeft = true;
            else if (turnedLeft && _animationDirection.x > 0.0f)
                turnedLeft = false;

            if (!turnedUp && _animationDirection.y > 0.0f)
                turnedUp = true;
            else if (turnedUp && _animationDirection.y < 0.0f)
                turnedUp = false;

            if (turnedLeft && turnedUp)
                eAnimationDirection = EDirection.UpLeft;
            else if (turnedLeft && !turnedUp)
                eAnimationDirection = EDirection.DownLeft;
            else if (!turnedLeft && turnedUp)
                eAnimationDirection = EDirection.UpRight;
            else
                eAnimationDirection = EDirection.DownRight;
        }
        else
            eAnimationDirection = EDirection.Zero;

        Animate(eAnimationDirection);
    }

    public void SetSpeed(float _animationSpeedFactor) => animator.speed = 1.0f * _animationSpeedFactor;

    void Animate(EDirection _EAnimationDirection)
    {
        switch (_EAnimationDirection)
        {
            case EDirection.Zero:
                animator.SetFloat("Magnitude", 0.0f);
                break;

            case EDirection.DownRight:
                animator.SetFloat("Vertical", 0.0f);
                spriteTransform.rotation = Quaternion.Euler(0, 0, 0);
                animator.SetFloat("Magnitude", 1.0f);
                break;

            case EDirection.UpRight:
                animator.SetFloat("Vertical", 1.0f);
                spriteTransform.rotation = Quaternion.Euler(0, 0, 0);
                animator.SetFloat("Magnitude", 1.0f);
                break;

            case EDirection.UpLeft:
                animator.SetFloat("Vertical", 1.0f);
                spriteTransform.rotation = Quaternion.Euler(0, 180.0f, 0);
                animator.SetFloat("Magnitude", 1.0f);
                break;

            case EDirection.DownLeft:
                animator.SetFloat("Vertical", 0.0f);
                spriteTransform.rotation = Quaternion.Euler(0, 180.0f, 0);
                animator.SetFloat("Magnitude", 1.0f);
                break;
        }
    }
}
