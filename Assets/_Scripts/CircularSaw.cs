using System.Collections;
using UnityEngine;

public class CircularSaw : MonoBehaviour
{
    [SerializeField] Transform[] Anchors;
    [SerializeField] float Speed = 1f;
    [SerializeField] float PauseDuration = 0.5f;

    ItemSoundManager itemSoundManager;

    int currentAnchor = 0;
    private Vector3 targetPosition;

    private void Start()
    {
        itemSoundManager = gameObject.GetComponent<ItemSoundManager>();
        itemSoundManager.PlaySawSound();
        StartCoroutine(PlaySoundAtIntervals());
        NextTarget();
    }

    //indefinitly trigger saw sound
    private IEnumerator PlaySoundAtIntervals()
    {
        yield return new WaitForSeconds(Random.Range(0, 20));
        itemSoundManager.PlaySawHitSound();
        StartCoroutine(PlaySoundAtIntervals());
    }

    private void NextTarget()
    {
        targetPosition = Anchors[currentAnchor].position;
        float duration = (transform.position - targetPosition).magnitude / Speed;
        StartCoroutine(LerpPosition(duration));
    }

    IEnumerator LerpPosition(float duration)
    {
        Vector3 startPosition = transform.position;

        float time = 0f;


        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (2f -  t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        yield return new WaitForSeconds(PauseDuration);

        currentAnchor++;
        if (currentAnchor >= Anchors.Length)
            currentAnchor = 0;
        NextTarget();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < Anchors.Length; i++)
        {
            int j = i + 1;
            if (j >= Anchors.Length)
                j = 0;

            Gizmos.DrawLine(Anchors[i].position, Anchors[j].position);
        }
    }
}

