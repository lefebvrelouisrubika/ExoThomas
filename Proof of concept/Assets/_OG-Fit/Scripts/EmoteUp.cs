using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.rotation = Quaternion.Euler(0,0,-transform.parent.gameObject.transform.rotation.z);
    }
}
