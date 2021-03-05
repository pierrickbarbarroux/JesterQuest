using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(IAnimationController))]
[RequireComponent(typeof(VisionDetection))]

public class EnnemyAI : MonoBehaviour
{

    [SerializeField] private float pauseBetweenPatrols;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackAngle = 5f;

    private Patroller patroller;
    private IAnimationController animationController;
    private VisionDetection detection;
    private EnnemySoundManager soundManager;

    private float timeForNewSound;
    private Transform player;
    private bool NextPatrolRequested = false;
    private bool playerFound = false;
    private bool chasingPlayer = false;
    private bool isAttacking = false;

    private void Start()
    {
        patroller = GetComponent<Patroller>();
        animationController = GetComponent<IAnimationController>();
        detection = GetComponent<VisionDetection>();
        player = GameObject.FindWithTag("Player").transform;
        soundManager = GetComponent<EnnemySoundManager>();
        playerFound = false;
        PlaySoundAtIntervals();
    }

    private void Update()
    {
        if (!chasingPlayer)
            playerFound = detection.targetingPlayer;
        
        if (playerFound && !chasingPlayer)
        {
            soundManager.PlayTriggeredSound();

            patroller.StopPatrol();
            chasingPlayer = true;
            patroller.ChasePlayer();

            detection.ChangeRangeValue(attackRange);
            detection.ChangeAngleValue(attackAngle);
            detection.hide = true;
            detection.targetingPlayer = false;
        }
        
        animationController.SetVelocity(patroller.normalizedMoveSpeed);

        if (chasingPlayer)
        {
            if (!isAttacking && detection.targetingPlayer)
            {
                patroller.PauseChase();
                isAttacking = true;
                animationController.SetAttack(player);
                soundManager.PlayPreAttackSound();
            }
            return;
        }

        if (!patroller.isCurrentlyPatrolling && !NextPatrolRequested)
        {
            StartCoroutine(NextPatrol());
        }


    }

    private IEnumerator NextPatrol()
    {
        NextPatrolRequested = true;
        yield return new WaitForSeconds(pauseBetweenPatrols);
        patroller.PatrolToNew();
        yield return new WaitForEndOfFrame();
        NextPatrolRequested = false;
    }


    public void AttackFinished()
    {
        StartCoroutine(AttackFinishedCoroutine());
    }

    private IEnumerator AttackFinishedCoroutine()
    {
        patroller.ResumeChase();
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    //indefinitly trigger iddle sound
    private IEnumerator PlaySoundAtIntervals()
    {
        yield return new WaitForSeconds(Random.Range(0, 30));
        soundManager.PlayIddleSound();
        StartCoroutine(PlaySoundAtIntervals());
    }
}
