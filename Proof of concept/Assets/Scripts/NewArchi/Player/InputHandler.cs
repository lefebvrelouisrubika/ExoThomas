using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("InputValue")]
    public float mouvHori = 0;
    public float mouvVert = 0;

    [Space(10)]
    public bool SideUp = false;
    public bool SideDown = false;
    [Space(5)]
    public bool HueUp = false;
    public bool HueDown = false;

    [Header("Additional Info")]
    public int inputCounter = 0;
    public static bool qwerty = false;
    public bool canHueChange = true;

    void Update()
    {
        InputCheck();
        inputCounter = CountInput();
    }

    private void InputCheck()
    {
        if (qwerty == false)
        {
            mouvHori = Input.GetAxis("Horizontal");
            mouvVert = Input.GetAxis("Vertical");

            SideUp = Input.GetKeyDown(KeyCode.Z);
            SideDown = Input.GetKeyDown(KeyCode.S);
            if (canHueChange)
            {
                HueUp = Input.GetKeyDown(KeyCode.Q);
                HueDown = Input.GetKeyDown(KeyCode.D);
            }
        }
        else if (qwerty == true)
        {
            mouvHori = Input.GetAxis("Horizontal");
            mouvVert = Input.GetAxis("Vertical");

            SideUp = Input.GetKeyDown(KeyCode.W);
            SideDown = Input.GetKeyDown(KeyCode.S);

            if (canHueChange)
            {
                HueUp = Input.GetKeyDown(KeyCode.A);
                HueDown = Input.GetKeyDown(KeyCode.D);
            }
        }

    }

    private int CountInput()
    {
        int counter = 0;

        if (SideUp)
        {
            counter++;
        }
        if (SideDown)
        {
            counter++;
        }

        if (HueUp)
        {
            counter++;
        }
        if (HueDown)
        {
            counter++;
        }
        return counter;
    }
}
