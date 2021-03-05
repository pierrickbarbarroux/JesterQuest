using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath : MonoBehaviour
{
    public Transform ennemyPath;
    public Transform player;
    public float speed;
    public float detectionRange;

    private Transform[] path;
    private Transform next_point;
    private bool ispatrolling;
    private Transform stopPatrollingPoint;
    private int index;
    private Vector3 dir;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ispatrolling = true;
        path = new Transform[ennemyPath.childCount];
        for (int i = 0; i < ennemyPath.childCount; i++)
        {
            path[i] = ennemyPath.GetChild(i);
        }
        index = 1;
        next_point = path[index];
        navMeshAgent.SetDestination(next_point.position);
       //ChangeDir();
    }

    // Update is called once per frame
    void Update()
    {
        if (ispatrolling && Input.GetKeyDown("space"))
        {
            StopPatrolling();
            return;
        }
        if (!ispatrolling && Input.GetKeyDown("space"))
        {
            GoToStopPatrollingPoint();
        }
        if (!ispatrolling)
        {
            return;
        }

        //if (ability.charging)
        //    return;

        //Vector3 playerDIr = player.position - transform.position;

        //if (playerDIr.magnitude <= detectionRange)
        //    ability.Charge(player, speed);


        if (Vector3.Distance(gameObject.transform.position, next_point.position) < 3.5)
        {
            Debug.Log("passé au prochain");
            if (next_point == stopPatrollingPoint)
            {
                next_point = path[index];
                navMeshAgent.SetDestination(next_point.position);
            }
            else
            {
                if (index == path.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
                next_point = path[index];
                navMeshAgent.SetDestination(next_point.position);

            }
            //ChangeDir();
        }

        //transform.Translate(dir.normalized * speed * Time.deltaTime);
    }

    public void StopPatrolling()
    {
        //définir un point où l'ennemy s'arrete de patrouiller
        stopPatrollingPoint.position = gameObject.transform.position;
        ispatrolling = false;
    }

    public void GoToStopPatrollingPoint()
    {
        ispatrolling = true;
        next_point = stopPatrollingPoint;
        navMeshAgent.SetDestination(next_point.position);
        //ChangeDir();
    }

    void ChangeDir()
    {
        dir = new Vector3(next_point.transform.position.x - transform.position.x, 0, next_point.position.z - transform.position.z);
    }
}
