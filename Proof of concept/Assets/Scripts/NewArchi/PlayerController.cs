using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerController : Shape
{
    private InputHandler input;
    private Rigidbody2D rb;

    [Header("BaseState")]
    public float baseSide = 3;
    [ColorUsage(false, false)]
    public Color baseColor = new Color(1f, 0.5f, 0.75f);

    [Header("Mouvement Parameter")]
    public float moveSpeed = 10f;
    public float turnSpeed = 10f;
    private Vector2 orientation = Vector2.up;

    [Header("Evolving Parameter")]
    public float sideEvolvSpeed = 1f;
    public float colorEvolvSpeed = 1f;
    public float returnBaseSideSpeed = 1f;
    public float returnBaseColorSpeed = 1f;

    private void Awake()
    {
        input = GetComponent<InputHandler>();
    }

    void Update()
    {
        Movement();

        Morphing();
        ChangeColor();

        ReturnToOriginal();
    }

    private void Movement()
    {
        //Mouv
        rb.velocity = new Vector2(input.mouvHori, input.mouvVert) * moveSpeed;

        //Orientation
        if(rb.velocity.magnitude > 0.1f)
        {
            orientation = rb.velocity.normalized;
        }
        
        float angle = Mathf.Atan2(-rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * turnSpeed);

    }

    void Morphing()
    {
        if (input.SideUp)
        {
            float nextSide = side + (sideEvolvSpeed * Time.deltaTime);
            side = Mathf.Clamp(nextSide, MinSide, MaxSide);

            rend.material.SetFloat("Sides", side);
        }
        if (input.SideDown)
        {
            float nextSide = side - (sideEvolvSpeed * Time.deltaTime);
            side = Mathf.Clamp(nextSide, MinSide, MaxSide);

            rend.material.SetFloat("Sides", side);
        }
    }

    void ChangeColor()
    {
        //Rouge
        if (input.RedUp)
        {
            amountR += colorEvolvSpeed;
        }
        if (input.RedDown)
        {
            amountR += colorEvolvSpeed;
        }
        amountR = Mathf.Clamp01(amountR);

        //Vert
        if (input.GreenUp)
        {
            amountG += colorEvolvSpeed;
        }
        if (input.GreenDown)
        {
            amountG += colorEvolvSpeed;
        }
        amountG = Mathf.Clamp01(amountG);

        //Bleue
        if (input.BlueUp)
        {
            amountB += colorEvolvSpeed;
        }
        if (input.BlueDown)
        {
            amountB += colorEvolvSpeed;
        }
        amountB = Mathf.Clamp01(amountB);
    }

    void ReturnToOriginal()
    {
        //Shape or Side
        if (!input.SideDown || !input.SideUp )
        {
            if(side != baseSide)
            {
                side = Mathf.Lerp(side, baseSide, Time.deltaTime * returnBaseSideSpeed);
                UpdateSide();
            }
        }

        //Color
        if (!input.RedUp || !input.RedDown || !input.GreenUp || !input.GreenDown|| !input.BlueUp || !input.BlueDown)
        {
            if (color != baseColor)
            {
                amountR = Mathf.Lerp(amountR, baseColor.r, Time.deltaTime * returnBaseColorSpeed);
                amountG = Mathf.Lerp(amountG, baseColor.g, Time.deltaTime * returnBaseColorSpeed);
                amountB = Mathf.Lerp(amountB, baseColor.b, Time.deltaTime * returnBaseColorSpeed);

                UpdateColor();
            }
        }
    }
}
