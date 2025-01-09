using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class BasicSceneMangement : MonoBehaviour
{
    [SerializeField, Scene] private int scene;

    [Button]
    public void LoadScene()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
    public void LoadScene(int index)
    {
        scene = index;
        LoadScene();
    }

    [Button]
    public void Quit()
    {
        if (Application.isEditor) 
        { 
            Debug.LogError("Application Quit"); 
        }
        else
        {
            Application.Quit();
        }
    }
}
