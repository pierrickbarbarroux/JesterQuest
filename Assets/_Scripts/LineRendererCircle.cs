using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererCircle : MonoBehaviour
{
	public int segments;
	public float xradiusMax;
	public float yradiusMax;
	public bool telegraphing=false;
	public ChargerAbility ca;
	public float time;

	private float xradius;
	private float yradius;
	LineRenderer line;

	void Start()
	{
		line = gameObject.GetComponent<LineRenderer>();
		line.useWorldSpace = false;
		xradiusMax = ca.radiusAttack;
		yradiusMax = ca.radiusAttack;
	}

    void Update() {
        //if (Input.GetKeyDown("a"))
        //{
        //    ca.SetRoar();
        //    ActivateTelegraphAnimation();
        //}
        //if (Input.GetKeyDown("b"))
        //{
        //    FadeTelegraph();
        //}
        if (!telegraphing)
        {
			return;
        }

		CreatePoints();
	}

	public void ActivateTelegraphAnimation()
    {
		xradius = 0f;
		yradius = 0f;
		line.positionCount = segments + 1;
		telegraphing = true;
	}

	public void FadeTelegraph()
    {
		telegraphing = false;
		line.positionCount = 0;
	}

	void CreatePoints()
	{
		if (xradius < xradiusMax && yradius < yradiusMax)
		{
			xradius += (xradiusMax / time) * Time.deltaTime;
			yradius += (yradiusMax / time) * Time.deltaTime;
		}

		float x;
		float y=0f;
		float z;

		float angle = 20f;

		for (int i = 0; i < (segments + 1); i++)
		{
			x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
			z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

			line.SetPosition(i, new Vector3(x, y, z));

			angle += (360f / segments);
		}
	}
}
