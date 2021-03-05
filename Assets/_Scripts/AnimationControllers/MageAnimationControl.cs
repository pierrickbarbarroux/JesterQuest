using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAnimationControl : MonoBehaviour, IAnimationController
{
    [SerializeField]
    private Animator animator;
    public GameObject projectile;
    public GameObject telegram;

    public bool isAttacking = false;


    float targetVelocity;
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("velocity", targetVelocity, .1f, Time.deltaTime);
    }

    public void SetAttack(Transform p)
    {
        isAttacking = true;
        SetVelocity(0f);
        animator.SetTrigger("Attack");
        //StartCoroutine(CastFireBall(p));

    }

    public void SetVelocity(float velocity)
    {
        targetVelocity = velocity;
        animator.SetFloat("velocity", velocity, .5f, Time.deltaTime);
    }

    public void SetDeath()
    {
        animator.SetBool("isDead", true);
    }

    public bool GetAttacking()
    {
        return true;
    }

    IEnumerator CastFireBall(Transform p)
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 position = p.position;


        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }

    public void InstantiateTelegram()
    {
        GameObject new_telegram = Instantiate(telegram, transform.position, transform.rotation);
        new_telegram.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("TelegramFireBall");
        Destroy(new_telegram, 1.5f);
    }

    public void InstantiateFireBall()
    {
        GameObject fireball = Instantiate(projectile, transform.position, transform.rotation);
        Destroy(fireball,2.1f);
    }
}
