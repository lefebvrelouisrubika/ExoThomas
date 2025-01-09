using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderZoomStreet : MonoBehaviour
{
    
    

    public float zoomAmount;
    float baseOrtho;

    void Awake()
    {
        
        baseOrtho = Camera.main.orthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (zoomAmount != 0.0f)
            {
                CameraManager.Instance.targetOrtho -= zoomAmount;

            }
        }


        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {

            CameraManager.Instance.targetOrtho = baseOrtho;


        }

    }

   

}
