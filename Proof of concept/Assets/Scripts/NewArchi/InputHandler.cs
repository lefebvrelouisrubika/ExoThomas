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
    public bool RedUp = false;
    public bool RedDown = false;
    [Space(5)]
    public bool GreenUp = false;
    public bool GreenDown = false;
    [Space(5)]
    public bool BlueUp = false;
    public bool BlueDown = false;

    void Update()
    {
        mouvHori = Input.GetAxis("Horizontal");
        mouvVert = Input.GetAxis("Vertical");

        SideUp = Input.GetButton("a");
        SideDown = Input.GetButton("q");

        RedUp = Input.GetButton("z");
        RedDown = Input.GetButton("s");

        GreenUp = Input.GetButton("e");
        GreenDown = Input.GetButton("d");

        BlueUp = Input.GetButton("r");
        BlueDown = Input.GetButton("f");
    }
}
