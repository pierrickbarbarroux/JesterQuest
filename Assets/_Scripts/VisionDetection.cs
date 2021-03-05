using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionDetection : MonoBehaviour
{
    public bool isLooking;
    [HideInInspector] public bool targetingPlayer;
    public bool forceTargetingPlayer;
    public float detectionAngle;
    public float detectionMaxDistance;
    public bool hide;

    public LineRenderer rightLR;
    public LineRenderer leftLR;
    public LineRenderer interLine;
    private int lengthOfLineRenderer;
    private int lengthOfInterLine;

    private Transform player;

    private Vector3 right;
    private Vector3 left;

    private Vector3 dirPlayer;
    private float angle;
    private float rad;

    private int layerMask;

    public MeshFilter meshFilter;
    public GameObject Vision;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;


    // Start is called before the first frame update
    void Start()
    {
        Vision.transform.localScale = new Vector3(Vision.transform.localScale.x / gameObject.transform.localScale.x, Vision.transform.localScale.y / gameObject.transform.localScale.y, Vision.transform.localScale.z / gameObject.transform.localScale.z);
        mesh = new Mesh();
        vertices = new Vector3[interLine.positionCount + 1];
        triangles = new int[3*(interLine.positionCount - 1)];
        meshFilter.mesh = mesh;

        if (player==null)
        {
            player = GameObject.Find("Player").transform;
        }
        ChangeDirPlayer();
        if (!hide)
        {
            lengthOfLineRenderer = rightLR.positionCount;
            lengthOfInterLine = interLine.positionCount;
        }

        rad= (detectionAngle*Mathf.PI)/180f;
        layerMask = LayerMask.GetMask(new string[] { "Default", "Wall" });

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //this.transform.Translate(new Vector3(-1, 0, 0) * 100f * Time.deltaTime);
        if (!isLooking)
        {
            return;
        }
        ChangeDirPlayer();
        angle = Vector3.Angle(dirPlayer, transform.forward);
        if (angle<=detectionAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dirPlayer, out hit, detectionMaxDistance))
            {
                if (hit.collider.gameObject.name=="Player")
                {
                    if (!targetingPlayer)
                    {
                        targetingPlayer = true;
                        if (!hide)
                        {
                            rightLR.startColor = Color.yellow;
                            rightLR.endColor = Color.yellow;
                            leftLR.startColor = Color.yellow;
                            leftLR.endColor = Color.yellow;
                            interLine.startColor = Color.yellow;
                            interLine.endColor = Color.yellow;
                        }
                    }
                }
                else
                {
                    if (targetingPlayer)
                    {
                        targetingPlayer = false;
                        if (!hide)
                        {
                            rightLR.startColor = Color.red;
                            rightLR.endColor = Color.red;
                            leftLR.startColor = Color.red;
                            leftLR.endColor = Color.red;
                            interLine.startColor = Color.red;
                            interLine.endColor = Color.red;
                        }
                    }
                        
                }
            }
            else
            {
                if (targetingPlayer)
                {
                    targetingPlayer = false;
                    if (!hide)
                    {
                        rightLR.startColor = Color.red;
                        rightLR.endColor = Color.red;
                        leftLR.startColor = Color.red;
                        leftLR.endColor = Color.red;
                        interLine.startColor = Color.red;
                        interLine.endColor = Color.red;
                    }
                }
            }
        }
        if(angle>detectionAngle && targetingPlayer)
        {
            targetingPlayer = false;
            targetingPlayer = false;
            if (!hide)
            {
                rightLR.startColor = Color.red;
                rightLR.endColor = Color.red;
                leftLR.startColor = Color.red;
                leftLR.endColor = Color.red;
                interLine.startColor = Color.red;
                interLine.endColor = Color.red;
            }
        }
        if (!hide)
        {
            Vision.SetActive(true);
            DrawVision();
            DrawMesh();
            UpdateMesh();
        }

        else
        {
            rightLR.gameObject.SetActive(false);
            leftLR.gameObject.SetActive(false);
            interLine.gameObject.SetActive(false);
            Vision.SetActive(false);
        }

        if (forceTargetingPlayer)
            targetingPlayer = true;
    }

    public void ChangeRangeValue(float newRange)
    {
        detectionMaxDistance = newRange;
    }
    public void ChangeAngleValue(float newAngle)
    {
        detectionAngle = newAngle;
    }
    void ChangeDirPlayer()
    {
        dirPlayer = player.position - transform.position;
    }

    void DrawVision()
    {
        var points_right = new Vector3[lengthOfLineRenderer];
        var points_left = new Vector3[lengthOfLineRenderer];
        var points_inter = new Vector3[lengthOfInterLine];
        points_right[0] = new Vector3(0, 0, 0);
        points_left[0] = new Vector3(0, 0, 0);
        right = (transform.forward*Mathf.Cos(rad) + transform.right* Mathf.Sin(rad));
        left = (transform.forward * Mathf.Cos(rad) - Mathf.Sin(rad) * transform.right);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, right, out hit, detectionMaxDistance, layerMask))
        {
            Debug.DrawRay(transform.position, right, Color.blue);
            points_right[lengthOfLineRenderer-1] =new Vector3(Mathf.Sin(rad),0, Mathf.Cos(rad)).normalized * hit.distance;
        }
        else
        {
            points_right[lengthOfLineRenderer-1] = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad)).normalized * detectionMaxDistance;
        }

        RaycastHit hit2;
        if (Physics.Raycast(transform.position, left, out hit2, detectionMaxDistance, layerMask))
        {
            points_left[lengthOfLineRenderer - 1] = new Vector3(-Mathf.Sin(rad), 0, Mathf.Cos(rad)).normalized * hit2.distance;
        }
        else
        {
            points_left[lengthOfLineRenderer - 1] = new Vector3(-Mathf.Sin(rad), 0, Mathf.Cos(rad)).normalized * detectionMaxDistance;
        }

        
        for (int i=1; i< lengthOfInterLine;i++)
        {
            RaycastHit hit3;
            float new_rad = (rad * ((lengthOfInterLine/2)-i)) / (lengthOfInterLine/2);
            Vector3 dir = (transform.forward * Mathf.Cos(new_rad) - Mathf.Sin(new_rad) * transform.right).normalized;
            if (Physics.Raycast(transform.position, dir, out hit3, detectionMaxDistance, layerMask))
            {
                points_inter[i] = new Vector3(-Mathf.Sin(new_rad), 0, Mathf.Cos(new_rad)).normalized * hit3.distance;

            }
            else
            {
                points_inter[i] = new Vector3(-Mathf.Sin(new_rad), 0, Mathf.Cos(new_rad)).normalized * detectionMaxDistance;
            }
        }
        points_inter[0] = points_left[lengthOfLineRenderer - 1];
        points_inter[lengthOfInterLine-1]= points_right[lengthOfLineRenderer - 1];
        rightLR.SetPositions(points_right);
        leftLR.SetPositions(points_left);
        interLine.SetPositions(points_inter);
    }

    void DrawMesh()
    {
        vertices[0] = leftLR.GetPosition(0);

        for (int i = 1; i < vertices.Length; i++)
        {
            vertices[i] = interLine.GetPosition(i - 1);
        }
        int j = 1;
        for (int i = 0; i < interLine.positionCount - 1; i++)
        {
            triangles[3*i] = 0;
            triangles[(3*i) + 1] = j;
            triangles[(3*i) + 2] = j + 1;
            j++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

    }
}
