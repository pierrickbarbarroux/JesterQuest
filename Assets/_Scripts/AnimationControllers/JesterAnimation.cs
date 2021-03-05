using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    JesterSoundManager jesterSoundManager;
    bool wasDashing;
    bool wasAlive;

    void Start()
    {
        jesterSoundManager = gameObject.GetComponent<JesterSoundManager>();
        wasDashing = false;
        wasAlive = true;
    }

    public void SetVelocity(float v)
    {
        animator.SetFloat("velocity", v,.2f,Time.deltaTime);
    }

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }

    public void SetDashing(bool isDashing)
    {
        if (isDashing && ! wasDashing)
        {
            jesterSoundManager.PlayDashSound();
            wasDashing = true;
        }
        if (!isDashing && wasDashing)
            wasDashing = false;

        animator.SetBool("isDashing", isDashing);
    }

    public void SetDead()
    {
        if (wasAlive)
        {
            jesterSoundManager.PlayTakesDamageSound();
            wasAlive = false;
        }
        animator.SetBool("isDead", true);
    }
}
