using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAnimationControl : MonoBehaviour, IAnimationController
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private ParticleSystem fireParticles;

    [SerializeField]
    private List<ParticleSystem> wallParticles;

    [SerializeField]
    private ParticleSystem leftFootFire;

    [SerializeField]
    private ParticleSystem rightFootFire;

    [SerializeField]
    private ParticleSystem spikesPS;

    [SerializeField]
    private GameObject telegram;

    float targetVelocity;
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("velocity", targetVelocity, .1f, Time.deltaTime);
    }

    public void SpikesAttack()
    {
        spikesPS.Play();
    }

    public void SetRoar()
    {
        animator.SetTrigger("Roar");
        fireParticles.Play();
    }

    public void SetFall()
    {
        animator.SetTrigger("Fall");
    }

    public void SetVelocity(float velocity)
    {
        targetVelocity = velocity;
        animator.SetFloat("velocity", velocity, .5f, Time.deltaTime);
    }

    public void TriggerLeftFootParticle()
    {
        if(targetVelocity > .4f)
        leftFootFire.Play();
    }

    public void TriggerRightFootParticle()
    {
        if (targetVelocity > .4f)
            rightFootFire.Play();
    }

    public void SetAttack(Transform player)
    {
        SetRoar();
    }

    public void SetDeath()
    {
        SetFall();
    }

    public bool GetAttacking()
    {
        return true;
    }

    public void Telegram()
    {
        GameObject new_telegram = Instantiate(telegram, transform.position, transform.rotation);
        new_telegram.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("FadeCone");
        Destroy(new_telegram, 6f);
    }
}
