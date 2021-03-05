using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name =="Player")
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController compo))
            {
                //compo.AddToCounter
            }
            Taken();
        }
    }

    public void Taken()
    {
        //ajouter 1 au total d'objet collecté du joueur
        //bruit
        //animation
        //détruire l'objet
        Destroy(this.gameObject);
    }
}
