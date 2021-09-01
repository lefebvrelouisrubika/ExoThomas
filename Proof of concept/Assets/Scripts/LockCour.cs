using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCour : MonoBehaviour
{
    public GameObject gateIn;
    // Start is called before the first frame update


    private void Start()
    {
        gateIn.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            gateIn.SetActive(true);
        }
        
    }
}
