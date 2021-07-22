using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public static Scenemanager instance;
    // Start is called before the first frame update
    //#region transitions 
    //public Animator transition;

    //#endregion
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //private void StartGame()
    //{
    //    StartCoroutine(LoadLevel());

    //}
    //IEnumerator LoadLevel()
    //{
    //    //play animation
    //    transitionj1.SetTrigger("Changescene");
    //    transitionj2.SetTrigger("Changescene");
    //    yield return new WaitForSeconds(1);
    //    SceneManager.LoadScene(1);
    //}
    //private void GoCredits()
    //{
    //    transitionj1.SetTrigger("Changescene");
    //    transitionj2.SetTrigger("Changescene");
    //    SceneManager.LoadScene(2);
    //    Debug.Log("Credit");
    //}

    //private void Quit()
    //{
    //    Application.Quit();
    //    Debug.Log("Quit");
    //}
}
