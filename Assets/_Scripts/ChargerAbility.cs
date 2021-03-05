using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ChargerAbility : MonoBehaviour
{
    public float speedMultiplier;
    public float timeLimit;
    public ChargerAnimationControl animationControl;
    public LineRendererCircle telegraph;
    public bool charging;
    public bool attacking;

    public float radiusAttack;

    private bool justHit = false;


    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            AttackAround();
        }
    }

        public void Charge(Transform Target, float speed)
    {
        charging = true;
        justHit = false;

        StartCoroutine(ChargeCoroutine(Target, speed));
    }

    private IEnumerator ChargeCoroutine(Transform target, float speed)
    {
        float timeToStop = Time.time + timeLimit;
        animationControl.SetRoar();
        animationControl.SetVelocity(0f);
        yield return new WaitForSeconds(.5f);
        Vector3 direction = (target.position - transform.position).normalized;
        animationControl.SetVelocity(1f);
        
        while (!justHit && Time.time < timeToStop)
        {
            transform.Translate(direction * speed * speedMultiplier * Time.deltaTime);
            yield return null;
        }
        animationControl.SetVelocity(0f);
        animationControl.SetFall();

        yield return new WaitForSeconds(.5f);
        charging = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            justHit = true;
    }

    public void AttackAround()
    {
        attacking = true;
        StartCoroutine(AttackAroundCoroutine());

    }

    private IEnumerator AttackAroundCoroutine()
    {
        animationControl.SetRoar();
        telegraph.ActivateTelegraphAnimation();
        yield return new WaitForSeconds(5f);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusAttack);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.name == "Player")
            {
                Debug.Log("player hit");
            }   
        }
        yield return new WaitForSeconds(0.5f);
        telegraph.FadeTelegraph();
        attacking = false;
    }

}
