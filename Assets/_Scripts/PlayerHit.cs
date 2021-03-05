using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{
    public PlayerController playerController;
    public JesterAnimation jesterAnimation;
    public Image image;
    //TODO animation, envoyer sur la bonne scene de fin
    private void OnTriggerStay(Collider other)
    {
        if (!playerController.isDashing && other.gameObject.layer == LayerMask.NameToLayer("Damage"))
        {
            StartCoroutine(End());
        }  
    }

    private IEnumerator End()
    {
        jesterAnimation.SetDead();
        playerController.SetKinematic();
        for(int i = 0; i < 60;i++)
        {
            image.color = new Color(0, 0, 0, (float)i / 60f);
            yield return null;
        }
        SceneManager.LoadScene(2);
    }
}
