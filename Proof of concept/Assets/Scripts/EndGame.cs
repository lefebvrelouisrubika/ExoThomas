using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] string sceneName = null;

    [Header("Fin")]
    bool finStarted = false;
    public Animator finUI;
    public Animator finDecors;

    public void Fin()
    {
        if (!finStarted)
        {
            finStarted = true;

            Debug.Log("fin");

            finUI.SetTrigger("Fin");
            finDecors.SetTrigger("Fin");
            StartCoroutine(TimeBeforeEnd());
        }
    }

    IEnumerator TimeBeforeEnd()
    {
        yield return new WaitForSeconds(13);
        SceneManager.LoadScene(sceneName);
    }
}
