using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    private PlayerController player;
    private Vector2 dist;

    [Header("Parameter")]
    [SerializeField] bool showEdge = true;
    [SerializeField] [ColorUsage(false, false)] Color edgeColor = Color.green;
    [SerializeField] Vector2 limit = Vector2.one;
    [Space(10)]
    [SerializeField] [Range(0f, 4f)] float MoveLerp = 1f;

    void Start()
    {
        player = PlayerController.instance;
    }


    void Update()
    {
        dist = player.transform.position - transform.position;

        //Horizontal Correction
        //Left
        if (dist.x < -limit.x)
        {
            transform.position += Vector3.left * Mathf.Abs(Mathf.Abs(dist.x) - limit.x);
        }
        //right
        else if (dist.x > limit.x)
        {
            transform.position += Vector3.right * Mathf.Abs(dist.x - limit.x);
        }

        //Vertical Correction
        //Top
        if (dist.y > limit.y)
        {
            transform.position += Vector3.up * Mathf.Abs(dist.y - limit.y);
        }
        //Bottom
        else if (dist.y < -limit.y)
        {
            transform.position += Vector3.down * Mathf.Abs(Mathf.Abs(dist.y) - limit.y);
        }

        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        transform.position = Vector3.Lerp(this.transform.position, targetPos, MoveLerp * Time.deltaTime);

    }


    private void OnDrawGizmos()
    {
        if (showEdge)
        {
            //_
            Debug.DrawLine(transform.position + new Vector3(limit.x, -limit.y, 0), transform.position + new Vector3(-limit.x, -limit.y, 0), edgeColor);
            //|_
            Debug.DrawLine(transform.position + new Vector3(-limit.x, -limit.y, 0), transform.position + new Vector3(-limit.x, limit.y, 0), edgeColor);
            //|=
            Debug.DrawLine(transform.position + new Vector3(-limit.x, limit.y, 0), transform.position + new Vector3(limit.x, limit.y, 0), edgeColor);
            //|=|
            Debug.DrawLine(transform.position + new Vector3(limit.x, limit.y, 0), transform.position + new Vector3(limit.x, -limit.y, 0), edgeColor);
        }
    }
}
