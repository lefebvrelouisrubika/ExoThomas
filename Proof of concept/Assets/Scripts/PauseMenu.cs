using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject optionMenu;
    public GameObject pauseButtons;
    public GameObject playerController;

    void Start()
    {
        pauseButtons.SetActive(true);
        optionMenu.SetActive(false);
        pauseCanvas.SetActive(false);
    }

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

    public void SetKeyboard (bool isQwerty)
    {
        InputHandler.qwerty = isQwerty;
    }

    public void ReturnToMenu(string scene)
    {
        SetPauseMenuState(false);
        Scenemanager.instance.ChangeScene(scene);
    }
}
