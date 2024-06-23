using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : EnemyPool
{
    [Header("Pathfinding")]
    [SerializeField] private float waypointDelay;
    [SerializeField] private List<Vector2> waypoints = new();
    private Stack<Vector2> waypointsStack = new();
    private Vector2 nextWaypoint;
    protected bool hasWaypoint = false;
    private bool moveToStart = false;
    private float delayTimer = 0;

    [Header("GoalPositionValue")]
    [SerializeField] private float toleranzValue;
    protected Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] protected float moveSpeed;

    protected Transform playerTransform;

    protected Vector2 startPosition;
    protected bool followPlayer = false;


    private void Awake()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    protected virtual void OnEnable()
    {
        waypoints.Clear();
        waypointsStack.Clear();
    }

    protected void GetWayPoints()
    {
        StartCoroutine(WayPointCoroutine());
    }

    private IEnumerator WayPointCoroutine()
    {
        yield return new WaitUntil(() =>
        {
            if (delayTimer < waypointDelay)
            {
                delayTimer += Time.deltaTime;
                return !followPlayer;
            }
            else
            {
                delayTimer = 0;
                Vector2 currentPosition = transform.position;
                waypoints.Add(currentPosition);
                waypointsStack.Push(currentPosition);
                return !followPlayer;
            }
        });

        delayTimer = 0;
    }

    protected void MoveToStart()
    {

        //hier noch ein fehler!
        //auserdem wegpunkt überprüfen ob eine wall im weg ist -> reycast zwischen wegpunkt und next wegpunkt? oder so currentpos und nextweg?
        if (!hasWaypoint && !moveToStart)
        {
            FindNextWaypoint();
        }
        else if (moveToStart)
        {
            MoveToStartPos();
        }
        else
        {
            MoveToWaypoint();
        }
    }

    private void FindNextWaypoint()
    {
        if (waypoints.Count > 0)
        {
            nextWaypoint = waypointsStack.Peek();
            hasWaypoint = true;
        }
        else
        {
            moveToStart = true;
        }
    }

    private void MoveToWaypoint()
    {
        Vector2 currentPosition = transform.position;
        float distance = Vector2.Distance(currentPosition, nextWaypoint);
        if (distance < toleranzValue)
        {
            waypoints.RemoveAt(waypoints.Count - 1);
            hasWaypoint = false;
            waypointsStack.Pop();
        }
        else
        {
            Vector2 dir = nextWaypoint - currentPosition;
            dir.Normalize();
            rb.velocity = moveSpeed * Time.fixedDeltaTime * dir;
        }
    }

    private void MoveToStartPos()
    {
        Vector2 currentPosition = transform.position;
        float distance = Vector2.Distance(currentPosition, startPosition);

        if (distance < toleranzValue)
        {
            transform.position = startPosition;
            rb.velocity = Vector2.zero;
            moveToStart = false;
        }
        else
        {
            Vector2 dir = startPosition - currentPosition;
            dir.Normalize();
            rb.velocity = moveSpeed * Time.fixedDeltaTime * dir;
        }
    }

    private bool CheckDistance(Vector2 waypoint)
    {
        Vector2 currentPosition = transform.position;
        float currentDistance = Vector2.Distance(currentPosition, startPosition);
        float waypointDistance = Vector2.Distance(waypoint, startPosition);

        if (currentDistance > waypointDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
