using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public float targetOrtho;
    public float smoothSpeed = 2.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        targetOrtho = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(targetOrtho);
        targetOrtho = Mathf.Clamp(targetOrtho, 2.5f, 6);
        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

    }
}
