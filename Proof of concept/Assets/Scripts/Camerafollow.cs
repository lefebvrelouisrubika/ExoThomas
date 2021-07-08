using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController Player;
    public float MoveLerp;

    void Start()
    {
        Player = PlayerController.instance;

    }
    // Update is called once per frame
    void Update()
    {
         Vector3 targetPos = new Vector3(Player.transform.position.x, Player.transform.position.y,-10);
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, MoveLerp*Time.deltaTime);
    }
}
