using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public static Scenemanager instance;

    public GameObject ImageFondu;


    #region transitions 
    public Animator transition;
    

    #endregion
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));

    }


    IEnumerator LoadLevel(string sceneName)
    {
        
        ImageFondu.SetActive(true);
        transition.SetTrigger("FonduOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("FonduIn");
        //Debug.Log("play animation");
        StartCoroutine(Dissapear());

    }
    IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(0.5f);
        ImageFondu.SetActive(false);
    }

    //private void GoCredits()
    //{
    //    transitionj1.SetTrigger("Changescene");
    //    transitionj2.SetTrigger("Changescene");
    //    SceneManager.LoadScene(2);
    //    Debug.Log("Credit");
    //}


    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }


}
