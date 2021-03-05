using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FadeTelegram : MonoBehaviour
{

    public float time;
    private SpriteRenderer spriteRenderer;

    private bool isfading;
    private bool isfadingout;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isfading && !isfadingout)
        {
            return;
        }
    }

    public void ActivateFading()
    {
        isfading = true;
    }

    public void ActivateFadingOut()
    {
        isfadingout = true;
    }

    private void Fading()
    {
        Color col = new Color(1,1,1,1);
        //spriteRenderer.color = col*; //(255f / time) * Time.deltaTime;
    }


}
