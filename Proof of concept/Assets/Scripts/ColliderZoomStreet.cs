using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderZoomStreet : MonoBehaviour
{
    float targetOrtho;
    public float smoothSpeed = 2.0f;

    public float zoomAmount;
    float baseOrtho;

    void Awake()
    {
        targetOrtho = Camera.main.orthographicSize;
        baseOrtho = Camera.main.orthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (zoomAmount != 0.0f)
            {
                targetOrtho -= zoomAmount;

            }
        }


        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {

                targetOrtho = baseOrtho;


        }

    }

    private void Update()
    {
        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
    }

}
