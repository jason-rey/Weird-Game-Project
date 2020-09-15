using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 200f;


    public float enemyJumpForce;
    public float nextWaypointDistance = 3f;
    public float pathRefreshTime;

    public float jumpBuffer;
    public float jumpDistanceToTarget;

    public Path path;

    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rgbd;

    private Collider2D enemyCollider;


    private GameObject Player;
    private stopTimeController timeController;


    private int layerMask;

    //public List<Vector3> listOfPoints = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rgbd = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        timeController = Player.GetComponent<stopTimeController>();

        enemyCollider = GetComponent<Collider2D>();

        layerMask = ~(LayerMask.GetMask("Enemy"));


        InvokeRepeating("UpdatePath", 0f, pathRefreshTime);        
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


    private void Update()
    {
        //listOfPoints = path.vectorPath;
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


        if (!timeController.timeIsStopped)
        {
            transform.Translate(new Vector3(targetVector.x,0));

            //Debug.Log(Vector2.Distance(rgbd.position, target.position));

            //Debug.Log(path.vectorPath[path.vectorPath.Count - 1].y - transform.position.y);
            if (path.vectorPath[path.vectorPath.Count - 1].y - transform.position.y >= jumpBuffer && Vector2.Distance(rgbd.position, target.position) <= jumpDistanceToTarget)
            {
                Debug.Log("want to jump");
                Jump();
            }

        }

        float distance = Vector2.Distance(rgbd.position, path.vectorPath[currentWaypoint]);


        IsGrounded();


        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

  
        }

        Debug.Log(IsGrounded());


    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, (enemyCollider.bounds.size.y / 2) + 0.1F,layerMask);
        Debug.DrawRay(this.transform.position, Vector2.down * ((enemyCollider.bounds.size.y / 2) + 0.1F));
        return hit;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, enemyJumpForce);
        }

    }



}
