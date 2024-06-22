using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : EnemyPool
{
    [Header("Pathfinding")]
    [SerializeField] private float waypointDelay;
    [SerializeField] private List<Vector2> waypoints = new();
    private Vector2 nextWaypoint;
    private bool hasWaypoint = false;
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
            Debug.Log("findwaypoint");
            FindNextWaypoint();
        }
        else if (moveToStart)
        {
            Debug.Log("movetostart");
            MoveToStartPos();
        }
        else
        {
            Debug.Log("MoveToWaypoint");
            MoveToWaypoint();
        }
    }

    private void FindNextWaypoint()
    {
        if (waypoints.Count > 0)
        {
            int wayPointIndex = waypoints.Count - 1;

            for (int i = wayPointIndex; i >= 0; i--)
            {
                nextWaypoint = waypoints[i];
                hasWaypoint = true;
                break;
                //if (CheckDistance(waypoints[i]))
                //{

                //}
                //else
                //{
                //    waypoints.RemoveAt(i);
                //}
            }
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
