using UnityEngine;

public class Enemy : EnemyAudio
{
    private Transform enemyContainer;
    [SerializeField] private LayerMask combinedLayerMask;
    [SerializeField] private float myRayCastHeight;
    [SerializeField] private float playerRayCastHeight;
    [SerializeField] private float damage;
    private bool isPlayerInRange = false;

    private Vector2 dir;
    private bool getWayPointsStarted = false;

    //PlayerStuff
    private IDamageable damageable;

    [Space]
    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private Collider2D hitPlayerTrigger;
    [SerializeField] private CircleCollider2D followPlayerTrigger;
    [SerializeField] private Animator animator;

    [SerializeField] private Collider2D groundCollider;

    [Space]
    [SerializeField] Character_AnimationController animationController;

    protected override void OnEnable()
    {
        base.OnEnable();
        transform.SetParent(enemyContainer);
        transform.position = startPosition;
        enemyCollider.enabled = true;
        groundCollider.enabled = true;
        animator.enabled = true;
        rb.isKinematic = false;
        rb.velocity = Vector2.zero;
        isPlayerInRange = false;
        hitPlayerTrigger.enabled = true;
    }

    protected override void Start()
    {
        base.Start();
        enemyContainer = GameObject.FindWithTag("EnemyContainer").transform;
        transform.SetParent(enemyContainer);
    }

    protected override void Update()
    {
        base.Update();
        animationController.SetDirection(rb.velocity);
        Debug.Log(rb.velocity);
    }

    private void FixedUpdate()
    {
        if (isPlayerInRange)
        {
            if (SeePlayer())
            {
                hasWaypoint = false;
                FollowPlayer();
            }
            else
            {
                followPlayer = false;
                getWayPointsStarted = false;

                MoveToStart();
            }
        }
        else
        {
            followPlayer = false;
            getWayPointsStarted = false;

            if ((Vector2)transform.position != startPosition)
            {
                MoveToStart();
            }
        }
    }

    private bool SeePlayer()
    {
        Vector2 tempPos = new Vector2(transform.position.x, transform.position.y + myRayCastHeight);
        Vector2 tempPlayerPos = new Vector2(playerTransform.position.x, playerTransform.position.y + playerRayCastHeight);
        dir = tempPlayerPos - tempPos;
        Vector2 currentPosition = transform.position;
        float distance = Vector2.Distance(currentPosition, tempPlayerPos);
        RaycastHit2D hit = Physics2D.Raycast(tempPos, dir, distance, combinedLayerMask);


        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(tempPos, dir, Color.green);
                return true;
            }
            else
            {
                Debug.DrawRay(tempPos, dir, Color.red);
                return false;
            }
        }
        else
        {
            Debug.DrawRay(tempPos, dir, Color.yellow);
            return false;
        }
    }

    private void FollowPlayer()
    {
        followPlayer = true;
        rb.velocity = moveSpeed * Time.fixedDeltaTime * dir.normalized;

        if (!getWayPointsStarted)
        {
            getWayPointsStarted = true;
            GetWayPoints();
        }
    }

    private void HitPlayer()
    {
        hitPlayerTrigger.enabled = false; 
        damageable.GetEnemy(this.transform);
        damageable.GetDamage(damage);
        rb.velocity = Vector2.zero;
        enemyCollider.enabled = false;
        groundCollider.enabled = false;
        rb.isKinematic = true;
        animator.enabled = false;
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("spawntrigger");
            if (damageable == null)
            {
                collision.gameObject.TryGetComponent(out damageable);
            }

            if (collision.IsTouching(hitPlayerTrigger))
            {
                HitPlayer();
                AddEnemyInPool(this);
            }
            if (collision.IsTouching(followPlayerTrigger))
            {
                isPlayerInRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
