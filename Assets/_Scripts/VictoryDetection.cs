using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VictoryDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var p = other.GetComponentInChildren<PlayerController>();
            if(p.hasPipo)
            {
                Victory();
            }
        }
    }

    private void Victory()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
