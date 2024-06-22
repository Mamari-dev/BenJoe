using UnityEngine;

public class Enemy : Pathfinding
{
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private LayerMask combinedLayerMask;
    [SerializeField] private float damage;
    private bool isPlayerInRange = false;

    private Vector2 dir;
    private bool getWayPointsStarted = false;

    //PlayerStuff
    private IDamageable damageable;

    [Header("Collider")]
    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private CircleCollider2D hitPlayerTrigger;
    [SerializeField] private CircleCollider2D followPlayerTrigger;

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
    }

    private void Start()
    {
        enemyContainer = GameObject.FindWithTag("EnemyContainer").transform;
    }

    private void Update()
    {
        animationController.SetDirection(rb.velocity);
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
        dir = playerTransform.position - transform.position;
        Vector2 currentPosition = transform.position;
        float distance = Vector2.Distance(currentPosition, playerTransform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, combinedLayerMask);


        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, dir, Color.green);
                return true;
            }
            else
            {
                Debug.DrawRay(transform.position, dir, Color.red);
                return false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, dir, Color.yellow);
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
        damageable.GetEnemy(this.transform);
        damageable.GetDamage(damage);
        rb.velocity = Vector2.zero;
        this.enabled = false;
        enemyCollider.enabled = false;
        groundCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
