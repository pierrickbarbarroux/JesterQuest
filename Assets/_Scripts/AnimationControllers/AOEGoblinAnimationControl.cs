using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AOEGoblinAnimationControl : MonoBehaviour, IAnimationController
{
    [SerializeField]
    private Animator animator;
    [SerializeField] EnnemySoundManager soundManager;
    public GameObject projectile;
    public FlasqueFX flasqueExplosion;
    public GameObject telegram;

    public bool isAttacking=false;

    private Transform currentTransform;
    private Vector3 currentPosition;
    private GameObject flasque;
    private GameObject new_telegram;
    float targetVelocity;
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("velocity", targetVelocity, .1f, Time.deltaTime);
    }

    public void SetAttack(Transform p)
    {
        isAttacking = true;
        SetRunning(false);
        SetVelocity(0f);
        //GetComponent<NavMeshAgent>().enabled =false;
        animator.SetTrigger("Throw");
        currentTransform = p;
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }

    public void SetVelocity(float velocity)
    {
        targetVelocity = velocity;
        animator.SetFloat("velocity", velocity, .5f, Time.deltaTime);
        SetRunning(velocity > 0);
    }

    public void SetDeath()
    {
        animator.SetBool("isDead",true);
    }

    public void Telegram()
    {
        currentPosition = currentTransform.position;
        soundManager.PlayPreAttackSound();
        new_telegram = Instantiate(telegram, currentPosition, Quaternion.identity);
        new_telegram.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Fade");
    }

    public void Flasque()
    {
        flasque = Instantiate(projectile, currentPosition, Quaternion.identity);
        StartCoroutine(ExplodeFlaque());
    }

    public IEnumerator ExplodeFlaque()
    {
        yield return new WaitForSeconds(.5f);
        soundManager.PlayPostAttackSound();
        Destroy(new_telegram);
        Destroy(flasque);
        FlasqueFX explosion = Instantiate(flasqueExplosion, currentPosition, Quaternion.identity);
        explosion.Trigger();
        Collider[] hitColliders = Physics.OverlapSphere(currentPosition, 5f);
        Destroy(explosion.gameObject, 5f);
    }
    public bool GetAttacking()
    {
        return isAttacking;
    }
}
