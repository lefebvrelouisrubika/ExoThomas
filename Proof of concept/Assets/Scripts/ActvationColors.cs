using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActvationColors : MonoBehaviour
{
    // Start is called before the first frame update
public InputHandler  Inputs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inputs.canHueChange = true;
    }
}
