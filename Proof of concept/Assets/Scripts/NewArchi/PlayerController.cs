using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerController : Shape
{
    private InputHandler input;
    private Rigidbody2D rb;

    [Header("Mouvement Parameter")]
    public float moveSpeed;
    public float turnSpeed;
    private Vector2 orientation;

    private void Awake()
    {
        input = GetComponent<InputHandler>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //Mouv
        rb.velocity = new Vector2(input.mouvHori, input.mouvVert) * moveSpeed;

        //Orientation
        if(rb.velocity.magnitude > 0.1f)
        {orientation = rb.velocity.normalized;}
        
        float angle = Mathf.Atan2(-rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * turnSpeed);

    }
}
