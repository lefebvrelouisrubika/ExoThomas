using UnityEngine;

public class MenuOption : MonoBehaviour
{

    [Header("Switch keyboard")]
    public GameObject azerty;
    public GameObject qwerty;

    void Start()
    {
        
    }

    void Update()
    {
        if (InputHandler.qwerty)
        {
            azerty.SetActive(false);
            qwerty.SetActive(true);
        }
        else
        {
            azerty.SetActive(true);
            qwerty.SetActive(false);
        }
    }

    public void SwitchKeyboard()
    {
        InputHandler.qwerty = !InputHandler.qwerty;
    }
    public void SwitchKeyboard(bool onQwerty)
    {
        InputHandler.qwerty = onQwerty;
    }
}
