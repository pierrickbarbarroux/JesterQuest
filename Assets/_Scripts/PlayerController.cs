using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rigi;
    private MusicManager musicManager;


    [SerializeField]
    private Transform model;

    [SerializeField]
    private Transform cameraTransform;

    [Header("Dash Parameters")]

    public float dashVelocity = 20f;
    public float dashTime = .5f;
    public float dashCooldown = 2f;

    public int dashMode = 0;

    private float dashTimer;

    public bool isDashing = false;
    private bool canDash = true;


    public float maxVelocity = 5;
    public float acceleration = .2f;
    public float deceleration = .1f;


    private Vector3 dashVector = Vector3.zero;
    private Vector3 motionVector = Vector3.zero;

    [SerializeField]
    private ParticleSystem dashParticles;
    [SerializeField]
    private Material dashMaterial;
    [SerializeField]
    private Renderer jesterRenderer;

    [SerializeField]
    private JesterAnimation jesterAnimation;

    private Material mat1;
    private Material mat2;

    public bool hasPipo;
    [SerializeField]
    private GameObject pipo;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        musicManager = GetComponent<MusicManager>();
        musicManager.PlayMusiqueAller();
    }

    // Update is called once per frame
    void Update()
    {
        if(rigi.velocity.magnitude > .1f)
            model.transform.forward = rigi.velocity.normalized - rigi.velocity.normalized.y * Vector3.up;
        motionVector = Input.GetAxisRaw("Vertical") * cameraTransform.forward + Input.GetAxisRaw("Horizontal") * cameraTransform.right;
        motionVector = motionVector.normalized;
        if(Input.GetButtonDown("Dash"))
        {
            Dash();
        }
    }

    public void SetPipo()
    {
        hasPipo = true;
        pipo.SetActive(true);
        musicManager.StopMusiqueAller();
        musicManager.PlayJingleFlute();
        StartCoroutine(PlayMusiqueRetour());

    }

    //indefinitly trigger saw sound
    private IEnumerator PlayMusiqueRetour()
    {
        yield return new WaitForSeconds(9);
        musicManager.PlayMusiqueRetour();
    }

    public void SetKinematic()
    {
        rigi.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            rigi.velocity = dashVector * dashVelocity;
            
        }
        else
        {
            var targetVelocity = maxVelocity * motionVector;
            var lerpCoef = rigi.velocity.magnitude > targetVelocity.magnitude ? deceleration : acceleration;
            var ry = rigi.velocity.y;
            rigi.velocity = Vector3.Lerp(rigi.velocity, targetVelocity, lerpCoef);
            rigi.velocity += Vector3.up * (ry - rigi.velocity.y - 9.81f);
            if(rigi.velocity.magnitude > .1f)
            {
                
                jesterAnimation.SetVelocity(rigi.velocity.magnitude / maxVelocity);
            }
            jesterAnimation.SetRunning(rigi.velocity.magnitude > .1f);
            
        }
    }


    private void Dash()
    {
        if(Time.time - dashTimer > dashCooldown)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        if(dashMode == 0)
        {
            isDashing = true;
            dashParticles.Play();
            GetComponent<AudioSource>().Play();
            dashVector = model.forward;
            jesterAnimation.SetDashing(true);
            if(mat1 == null)
            {
                mat1 = jesterRenderer.material;
                mat2 = jesterRenderer.materials[1];
            }
            jesterRenderer.materials = new Material[] { dashMaterial, dashMaterial };
            gameObject.layer = 11;
            var dashDistance = dashTime * dashVelocity;
            if (Physics.Raycast(transform.position,dashVector,out RaycastHit hit, dashDistance,LayerMask.GetMask("Wall")))
            {
                yield return new WaitForSeconds(hit.distance / dashVelocity);
            }
            else 
                yield return new WaitForSeconds(dashTime);
            dashParticles.Stop();
            jesterAnimation.SetDashing(false);
            rigi.velocity = maxVelocity * dashVector;
            dashTimer = Time.time;
            isDashing = false;
            gameObject.layer = 9;
            jesterRenderer.materials = new Material[] { mat1, mat2 };
        }
        if(dashMode == 1)
        {
            isDashing = true;
            dashVector = Vector3.zero;
            yield return new WaitForSeconds(dashTime);
            dashParticles.Play();
            isDashing = false;
            transform.position += 2 * transform.forward;
        }
    }
}
