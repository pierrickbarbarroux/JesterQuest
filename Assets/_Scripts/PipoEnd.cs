using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipoEnd : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [Tooltip("Glisser le empty qui contient tous les monstres à faire spawn au retour !")]
    [SerializeField]
    private GameObject enemies;

    private void Start()
    {
        if (enemies)
            enemies.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponentInChildren<PlayerController>().SetPipo();
            CameraController.instance.Turn();
            animator.SetTrigger("Get");
            Destroy(gameObject, 3);
        }

        if (enemies)
            enemies.SetActive(true);
    }
}
