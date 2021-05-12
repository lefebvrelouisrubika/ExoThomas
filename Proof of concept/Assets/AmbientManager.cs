using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{

    public static AmbientManager instance;

    public State state = State.Neutral;
    Renderer rend;
    public Vector3 Neutral;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    //private void Update()
    //{
    //    switch (state)
    //    {
    //        case State.Neutral:
    //            rend.material.SetColor("ColorPrincipale", Vector3 Neutral);
    //            break;
    //        case State.Flee:
    //            rend.material.SetColor("_Color", new Vector4(RAmmount, GAmmount, BAmmount, 1));
    //            break;
    //        case State.Attack:
    //            rend.material.SetColor("_Color", new Vector4(RAmmount, GAmmount, BAmmount, 1));
    //            break;
    //        case State.Happy:
    //            rend.material.SetColor("_Color", new Vector4(RAmmount, GAmmount, BAmmount, 1));
    //            break;
    //        default:

    //            break;
    //    }
    //}

}
