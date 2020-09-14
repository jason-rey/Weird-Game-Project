using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rgbd;

    private GameObject Player;
    private stopTimeController timeController;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rgbd = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        timeController = Player.GetComponent<stopTimeController>();

        InvokeRepeating("UpdatePath", 0f, 1f);        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rgbd.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rgbd.position).normalized;
        Vector2 targetVector = direction * speed * Time.deltaTime;

        if (!timeController.timeIsStopped)
        {
            transform.Translate(new Vector3(targetVector.x,0));
        }

        float distance = Vector2.Distance(rgbd.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
  
    }
}
