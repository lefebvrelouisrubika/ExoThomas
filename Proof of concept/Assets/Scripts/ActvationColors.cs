using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActvationColors : MonoBehaviour
{
    // Start is called before the first frame update
    public InputHandler  Inputs;
    public GameObject ChromaticWheel;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Inputs.canHueChange = true;
            ChromaticWheel.SetActive(true);
        }
    }
}
