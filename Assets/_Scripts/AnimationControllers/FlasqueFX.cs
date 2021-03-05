using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlasqueFX : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem glassPS;

    [SerializeField]
    private ParticleSystem initialCranePS;

    [SerializeField]
    private ParticleSystem cranesPS;

    [SerializeField]
    private ParticleSystem flaquePS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Trigger()
    {
        glassPS.Play();
        initialCranePS.Play();
        cranesPS.Play();
        flaquePS.Play();

        //yield return new WaitForSeconds(3f);

        //glassPS.Stop();
        //initialCranePS.Stop();
        //cranesPS.Stop();
        //flaquePS.Stop();
    }
}
