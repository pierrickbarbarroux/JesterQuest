using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Patroller : MonoBehaviour
{
    [HideInInspector] public bool isCurrentlyPatrolling;
    [HideInInspector] public float normalizedMoveSpeed;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float MinTravel;
    [SerializeField] private float PatrolRadius;
    [Tooltip("Le pourcentage de la durée du mouvement nécessaire pour atteindre la vitesse maximale")]
    [Range(0.01f,1f)]
    [SerializeField] private float AccelerationPercentage;
    [Tooltip("Le pourcentage de la durée du mouvement nécessaire pour décelerer completement à 0")]
    [Range(0.01f, 1f)]
    [SerializeField] private float DecelarationPercentage;
    [Min(0.1f)]
    [SerializeField] private float RotationSpeed;
    [Tooltip("If the agent should avoid melee, set it to something bigger than 0")]
    [SerializeField] private float playerDistanceMin = 0f;

    private Coroutine currentPatrol;
    private Vector3 patrolCenter = Vector3.zero;
    private Vector3 targetPosition;
    private Transform playerTransform;
    public void PatrolToNew()
    {
        if (patrolCenter == Vector3.zero)
            patrolCenter = transform.position;
        bool valid;
        int iterations = 0;
        do
        {
            valid = true;
            Vector2 next2d = Random.insideUnitCircle;
            next2d *= Random.Range(0f, PatrolRadius);
            targetPosition = new Vector3(next2d.x, 0f, next2d.y);
            targetPosition += patrolCenter;

            if ((transform.position - targetPosition).magnitude < MinTravel)
                valid = false;
            else {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, (targetPosition - transform.position).normalized, out hit, (targetPosition - transform.position).magnitude, LayerMask.GetMask("Wall")))
                {
                    valid = false;
                }
            }

            if (iterations > 100)
            {
                return;
            }
            iterations++;
        } while (!valid);

        float duration = (transform.position - targetPosition).magnitude / MoveSpeed;
        isCurrentlyPatrolling = true;
        float angle = Vector3.Angle(transform.forward, targetPosition - transform.position);
        float rotationDuration = angle / RotationSpeed;
        currentPatrol = StartCoroutine(LerpPosition(duration, rotationDuration));
    }

    public void StopPatrol()
    {
        if (currentPatrol != null)
        {
            StopCoroutine(currentPatrol);
            isCurrentlyPatrolling = false;
            normalizedMoveSpeed = 0;
            agent.SetDestination(transform.position);
        }
    }

    IEnumerator LerpPosition(float duration, float rotationDuration)
    {

        float time = 0;

        Quaternion initialRotation = transform.rotation;
        transform.LookAt(targetPosition);
        Quaternion targetRotation = transform.rotation;
        transform.rotation = initialRotation;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            if (time < rotationDuration)
            {
                float tR = time / rotationDuration;
                transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, tR);
            }

            if (time < duration * AccelerationPercentage)
                normalizedMoveSpeed = Mathf.Lerp(0, 1, time / (duration * AccelerationPercentage));

            else if (time > duration *(1-DecelarationPercentage))
            {
                normalizedMoveSpeed = Mathf.Lerp(1, 0, (time - duration * (1 - DecelarationPercentage)) / (duration * DecelarationPercentage));
            }

            else
                normalizedMoveSpeed = 1f;

            float t = time / duration;
            t = t * t * (2f - t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        normalizedMoveSpeed = 0;
        transform.position = targetPosition;
        isCurrentlyPatrolling = false;
    }

    public void ChasePlayer()
    {
        agent.speed = MoveSpeed;
        agent.angularSpeed = RotationSpeed;
        
        normalizedMoveSpeed = 1f;
        isCurrentlyPatrolling = true;
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentPatrol = StartCoroutine(ChasePlayerCoroutine());
    }

    private bool pausedChase = false;
    public void PauseChase()
    {
        pausedChase = true;
    }

    public void ResumeChase()
    {
        pausedChase = false;
    }
    IEnumerator ChasePlayerCoroutine()
    {
        while (true)
        {
            while (!pausedChase && (transform.position - playerTransform.position).magnitude < playerDistanceMin)
            {
                normalizedMoveSpeed = 0f;
                agent.speed = 0;
                agent.SetDestination(playerTransform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((playerTransform.position - transform.position).normalized), RotationSpeed * Time.deltaTime);
                yield return null;
            }

            while (!pausedChase && (transform.position - playerTransform.position).magnitude >= playerDistanceMin)
            {
                normalizedMoveSpeed = 1f;
                agent.speed = normalizedMoveSpeed * MoveSpeed;
                agent.SetDestination(playerTransform.position);
                yield return null;
            }

            while (pausedChase)
            {
                normalizedMoveSpeed = 0f;
                agent.speed = 0;
                yield return null;
            }
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, PatrolRadius);
    }
}
