using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField]
    private Animator jesterAnimator;

    [SerializeField]
    private GameObject spotLight;

    void Start()
    {
        StartCoroutine(LightAndKill());
    }

    private IEnumerator LightAndKill()
    {
        yield return new WaitForSeconds(1.5f);
        spotLight.SetActive(true);
        jesterAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
    
}
