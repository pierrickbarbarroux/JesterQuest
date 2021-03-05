using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public Transform place_holders_parent;
    public GameObject collectable;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<place_holders_parent.childCount; i++)
        {
            Vector3 pos = place_holders_parent.GetChild(i).position;
            Instantiate(collectable, pos, Quaternion.identity);
        }
    }

}
