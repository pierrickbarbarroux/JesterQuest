using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    MusicManager musicManager;
    public bool victoryMenu = false;
    public bool defeatMenu = false;

    void Start()
    {
        musicManager = gameObject.GetComponent<MusicManager>();
        if (victoryMenu)
        {
            return;
        }
        else if (defeatMenu)
        {
            return;
        }
        musicManager.PlayMusiqueMenu();
        musicManager.PlayFauxSilence();
        StartCoroutine(LoadScene());
    }

    private bool launch = false;
    IEnumerator LoadScene()
    {
        //Begin to load the Scene you specify
        yield return new WaitForSeconds(.1f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {

                if (launch)
                {
                    asyncOperation.allowSceneActivation = true; 
                }       
            }
            yield return null;
        }
        
    }

    public void StartGame()
    {
        musicManager.PlayValidationSound();
        musicManager.StopMusiqueMenu();
        //SceneManager.LoadScene(1, LoadSceneMode.Single);
        launch = true;
    }

    public void QuitGame()
    {
        musicManager.PlaySelectionSound();
        musicManager.StopMusiqueMenu();
        Application.Quit();
    }

    public void GoToMenu()
    {
        musicManager.PlaySelectionSound();
        musicManager.StopMusiqueVictory();
    }
}
