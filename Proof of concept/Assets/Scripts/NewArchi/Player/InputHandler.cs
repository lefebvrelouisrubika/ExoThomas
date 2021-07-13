using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("InputValue")]
    public float mouvHori = 0;
    public float mouvVert = 0;
    [Space(10)]
    public bool SideUp = false;
    public bool SideDown = false;

    [Space(10)]
    public bool RGB = false;
    [Space(5)]
    public bool HueUp = false;
    public bool HueDown = false;
    [Space(5)]
    public bool RedUp = false;
    public bool RedDown = false;
    [Space(5)]
    public bool GreenUp = false;
    public bool GreenDown = false;
    [Space(5)]
    public bool BlueUp = false;
    public bool BlueDown = false;

    [Header("Additional Info")]
    public int inputCounter = 0;

    void Update()
    {
        InputCheck();
        inputCounter = CountInput();
    }

    private void InputCheck()
    {
        mouvHori = Input.GetAxis("Horizontal");
        mouvVert = Input.GetAxis("Vertical");

        if (RGB)
        {
            SideUp = Input.GetButton("a");
            SideDown = Input.GetButton("q");
        }
        else
        {
            SideUp = Input.GetKey(KeyCode.F);
            SideDown = Input.GetKey(KeyCode.S);
        }

        if (RGB)
        {
            RedUp = Input.GetButton("z");
            RedDown = Input.GetButton("s");

            GreenUp = Input.GetButton("e");
            GreenDown = Input.GetButton("d");

            BlueUp = Input.GetButton("r");
            BlueDown = Input.GetButton("f");
        }
        else
        {
            HueUp = Input.GetKeyDown(KeyCode.A);
            HueDown = Input.GetKeyDown(KeyCode.E);

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

        if (RedUp)
        {
            counter++;
        }
        if (RedDown)
        {
            counter++;
        }
        
        if (GreenUp)
        {
            counter++;
        }
        if (GreenDown)
        {
            counter++;
        }

        if (BlueUp)
        {
            counter++;
        }
        if (BlueDown)
        {
            counter++;
        }

        return counter;
    }
}
