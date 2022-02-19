using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    Tank tankScript;
    AIDestinationSetter destinationSetter;
    AIPath aiPath;
    Seeker seeker;
    Rigidbody2D rb;

    GameObject player;

    public Transform minMapValues;
    public Transform maxMapValues;
    [SerializeField] GameObject targetPositionObj;

    public float nextWayPointDistance = 1;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    [SerializeField] float speed = 2.5f;

    public enum State { PATROL, ATTACKING, RETREATING }
    public State state = State.PATROL;

    bool patrolPointGenerated = false;

    float attackEndReachedDistance;
    float patrolEndReachedDistance = 0;

    void Start()
    {
        tankScript = this.GetComponent<Tank>();
        player = GameObject.FindGameObjectWithTag("Player");

        destinationSetter = this.GetComponent<AIDestinationSetter>();
        aiPath            = this.GetComponent<AIPath>();
        seeker            = this.GetComponent<Seeker>();
        rb                = this.GetComponent<Rigidbody2D>();

        attackEndReachedDistance = aiPath.endReachedDistance;

        targetPositionObj.transform.position = GenerateRandomPosition();
        destinationSetter.target = targetPositionObj.transform;
    }

    Vector3 GenerateRandomPosition()
    {
        Vector3 _position = new Vector3(Random.Range(minMapValues.position.x, maxMapValues.position.x), Random.Range(minMapValues.position.y, maxMapValues.position.y), 0f);
        return _position;
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        AIMovementAndRotation();
    }

    private void AIMovementAndRotation()
    {
        switch(state)
        {
            case State.PATROL:
                aiPath.endReachedDistance = patrolEndReachedDistance;
                // if patrol point has not been generated
                if (patrolPointGenerated == false)
                {
                    // generate patrol point
                    targetPositionObj.transform.position = GenerateRandomPosition();
                    patrolPointGenerated = true;
                }

                // if not at the destination
                if(this.transform.position != destinationSetter.target.transform.position)
                {
                    // set destinationSetter to patrol point
                    destinationSetter.target = targetPositionObj.transform;
                }
                else
                {
                    // if at destination generate new position
                    patrolPointGenerated = false;
                }
                break;

            case State.ATTACKING:
                aiPath.endReachedDistance = attackEndReachedDistance;
                patrolPointGenerated = false;
                destinationSetter.target = player.transform;
                break;
        } 
    }
}
