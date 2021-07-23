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
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }


    IEnumerator LoadLevel(string sceneName)
    {
        //play animation
        ImageFondu.SetActive(true);
        transition.SetTrigger("FonduOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("FonduIn");
    }
    //private void GoCredits()
    //{
    //    transitionj1.SetTrigger("Changescene");
    //    transitionj2.SetTrigger("Changescene");
    //    SceneManager.LoadScene(2);
    //    Debug.Log("Credit");
    //}

    private void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
