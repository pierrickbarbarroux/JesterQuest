using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource spikeSound;
    [SerializeField] private float delayBeforeTrigger = .5f;
    [SerializeField] private float delayBeforeRetract = .5f;

    ItemSoundManager itemSoundManager;

    void Start()
    {
        itemSoundManager = gameObject.GetComponent<ItemSoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        itemSoundManager.PlaySpikesSound();
        StartCoroutine(waitBeforeTrigger());
    }

    private IEnumerator waitBeforeTrigger()
    {
        yield return new WaitForSeconds(delayBeforeTrigger);
        animator.SetTrigger("Trigger");
        yield return new WaitForSeconds(delayBeforeRetract);
        animator.SetTrigger("Retract");
    }


}
