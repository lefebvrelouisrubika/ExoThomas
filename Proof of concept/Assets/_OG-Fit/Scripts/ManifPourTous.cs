using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManifPourTous : MonoBehaviour
{
    public Transform croudSpawnPoint;
    public Transform croudEndPoint;
    public GameObject[] crouds;
    public float croudSpeed;
    //Ecarts nécessaires pour pas que les groupes se chevauchent lorsqu'ils respwan derrière.
    public float croudLeftSpace;
    public float croudRightSpace;

    // Start is called before the first frame update
    void Start()
    {
        croudSpawnPoint.position = new Vector3(crouds[0].transform.position.x - croudLeftSpace, crouds[0].transform.position.y, 0);
        croudEndPoint.position = new Vector3(crouds[crouds.Length - 1].transform.position.x + croudRightSpace, crouds[0].transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < crouds.Length; i++)
        {
            crouds[i].transform.position = crouds[i].transform.position + Vector3.right * Time.deltaTime * croudSpeed;

            if (crouds[i].transform.position.x > croudEndPoint.position.x)
            {
                crouds[i].transform.position = new Vector3 (croudSpawnPoint.position.x, crouds[i].transform.position.y, crouds[i].transform.position.z);
            }
        }

    }
}
