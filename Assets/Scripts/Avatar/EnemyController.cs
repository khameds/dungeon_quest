using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyController : MonoBehaviour
{
    // TODO : get this from players list
    public GameObject target;        // Chased target gameobject
    public float updateRate = 2f;   // Update rate of A* path
    private Seeker seeker;          // Pathing script
    private Rigidbody2D rb;         // Referance to the Rigidbody2D component of the IA
    public Path path;               //The calculated path  
    public float speed = 300f;      //The AI's speed per second
    public ForceMode2D fMode;

    public bool isChasing = true;

    [HideInInspector] public bool pathIsEnded = false;

    // The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    void Start()
    {       
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
          
        // Start a new path to the target position, return the result to the OnPathComplete method
        seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
        isChasing = true;
        StartCoroutine(UpdatePath());
    }

    public GameObject GetTarget()
    {
        if (isChasing)
        {
            List<GameObject> tmp = new List<GameObject>();
            List<GameObject> targets = new List<GameObject>();
            tmp.AddRange(GameObject.FindGameObjectsWithTag("Player"));

            if (tmp.Count > 0)
            {
                foreach (GameObject i in tmp)
                {
                    if (!(i.GetComponent<PlayerHealth>().isDead))
                        targets.Add(i);
                }

                if (targets.Count == 0)
                {// All players are dead
                    isChasing = false;
                    target = null;
                }
                else
                {// Get a random target in alive targets
                    target = targets[Random.Range(0, targets.Count)];
                }
                return target;
            }
            else
            {
                isChasing = false;
                return null;
            }
        }
        else return null;
    }

    IEnumerator UpdatePath()
    {
        if (isChasing)
        {
            if (target == null) // TODO test if player alive
            {
                target = GetTarget();

                if (target == null)
                {
                    Debug.LogError("[UpdatePath] Can't find any player alive");
                    isChasing = false;
                    yield break;
                }
            }

            // Start a new path to the target position, return the result to the OnPathComplete method
            seeker.StartPath(transform.position, target.transform.position, OnPathComplete);

            yield return new WaitForSeconds(1f / updateRate);
            
        }

        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p)
    {
        if (isChasing && !p.error )
        {
            path = p;
            currentWaypoint = 0;
        }
        else Debug.Log("[EnemyController] OnPathComplete : Got a path with an error.");
        
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            if (target == null)
            {// Should not happen
                target = GetTarget();
                if (target == null)
                {
                    //print("[EnemyController] FixedUpdate : Target is null.");
                    return;
                }
            }

            if (path == null)
            {
                //print("[EnemyController] FixedUpdate : Path is null.");
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                pathIsEnded = true;
                //print("[EnemyController] FixedUpdate : Path ended.");
                return;
            }
            pathIsEnded = false;

            //Direction to the next waypoint
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            dir *= speed * Time.fixedDeltaTime;

            //Move the AI
            rb.AddForce(dir, fMode);
            //Debug.Log("Add force to " + this);

            float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (dist < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }
        }
    }
}
