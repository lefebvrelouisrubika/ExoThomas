using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject optionMenu;
    public GameObject pauseButtons;
    public GameObject playerController;

    // Start is called before the first frame update
    void Start()
    {
        pauseButtons.SetActive(true);
        optionMenu.SetActive(false);
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.activeSelf == false)
            {
                SetPauseMenuState(true);
            }

            else if (pauseCanvas.activeSelf == true)
            {
                SetPauseMenuState(false);
            }
        }
    }

    public void SetPauseMenuState(bool state)
    {
        pauseCanvas.SetActive(state);

        if (state == false)
        {
            Time.timeScale = 1f;
        }
        else if (state == true)
        {
            Time.timeScale = 0f;
        }
    }

    public void SetOptionMenuState(bool state)
    {
        optionMenu.SetActive(state);

        if (state == true)
        {
            pauseButtons.SetActive(false);
        }
        else if (state == false)
        {
            pauseButtons.SetActive(true);
        }
    }

    public void SetKeyboard (bool qwerty)
    {
        playerController.GetComponent<InputHandler>().qwerty = qwerty;
    }

    public void ReturnToMenu(string scene)
    {
        SetPauseMenuState(false);
        Scenemanager.instance.ChangeScene(scene);
    }
}
