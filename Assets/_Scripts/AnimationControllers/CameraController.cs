using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        else instance = this;
    }

    public float acceleration = 2;
    public Transform targetToFollow;
    public bool isFollowing;
    private bool canFollow = true;

    private Vector3 offset;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float targetRotation;
    private float rotation;

    private void Start()
    {
        if(targetToFollow)
        {
            offset = transform.position - targetToFollow.position;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetToFollow.position + offset, acceleration/20f);
        rotation = Mathf.Lerp(rotation, targetRotation, .05f);
        transform.rotation = Quaternion.Euler(0,rotation,0);
    }

    public void Turn()
    {
        rotation = targetRotation;
        targetRotation += 180;
        
    }

    public void SetTarget(Transform targetToFollow,Vector3 offset)
    {
        this.offset = offset;
        this.targetToFollow = targetToFollow;
        isFollowing = true;
    }

    public void Shake(float duration = .20f, float strength = .6f, float frequency = 1f)
    {
        StartCoroutine(ShakeCoroutine(duration, strength, frequency));
    }

    private IEnumerator ShakeCoroutine(float duration = .20f, float strength = .6f, float frequency = 1f)
    {
        Vector3 originalPosition = cameraTransform.localPosition;

        float elapsed = 0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * strength;
            float y = Random.Range(-1f, 1f) * strength;
            float z = Random.Range(-1f, 1f) * strength;

            cameraTransform.localPosition = originalPosition + new Vector3(x, y, z);
 
            for(int i = 0;i< 1f/frequency; i++)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        cameraTransform.localPosition = originalPosition;
    }

}
