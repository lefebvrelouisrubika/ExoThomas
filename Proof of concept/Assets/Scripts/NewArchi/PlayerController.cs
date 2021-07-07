using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerController : Shape
{
    [Header("Components")]
    public InputHandler input;
    public Rigidbody2D rb;

    [Header("BaseState")]
    public float baseSide = 3;

    [ColorUsage(false, false)]
    public Color baseColor = new Color(1f, 0.5f, 0.75f);

    [Header("Mouvement Parameter")]
    public float moveSpeed = 10f;

    public float turnSpeed = 10f;
    [Range(0,360)]
    public float baseAngle = 0f;
    public Vector2 orientation = Vector2.right;

    [Header("Evolving Parameter")]
    public float sideEvolvSpeed = 1f;

    public float colorEvolvSpeed = 1f;
    public float returnBaseSideSpeed = 1f;
    public float returnBaseColorSpeed = 1f;

    public override void Awake()
    {
        base.Awake();

        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Orientation();

        Morphing();
        ChangeColor();

        ReturnToOriginal();
    }

    private void Movement()
    {
        //Mouv
        rb.velocity = new Vector2(input.mouvHori, input.mouvVert) * moveSpeed;
    }

    private void Orientation()
    {
        float angle = Mathf.Atan2(-input.mouvHori, input.mouvVert) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, baseAngle + angle), Time.deltaTime * turnSpeed);
    }

    private void Morphing()
    {
        if (input.SideUp)
        {
            float nextSide = side + (sideEvolvSpeed * Time.deltaTime);
            side = Mathf.Clamp(nextSide, MinSide, MaxSide);
        }
        if (input.SideDown)
        {
            float nextSide = side - (sideEvolvSpeed * Time.deltaTime);
            side = Mathf.Clamp(nextSide, MinSide, MaxSide);

        }
        UpdateSide();
    }

    private void ChangeColor()
    {
        //Rouge
        if (input.RedUp)
        {
            amountR += colorEvolvSpeed * Time.deltaTime;
        }
        if (input.RedDown)
        {
            amountR -= colorEvolvSpeed * Time.deltaTime;
        }
        amountR = Mathf.Clamp01(amountR);

        //Vert
        if (input.GreenUp)
        {
            amountG += colorEvolvSpeed * Time.deltaTime;
        }
        if (input.GreenDown)
        {
            amountG -= colorEvolvSpeed * Time.deltaTime;
        }
        amountG = Mathf.Clamp01(amountG);

        //Bleue
        if (input.BlueUp)
        {
            amountB += colorEvolvSpeed * Time.deltaTime;
        }
        if (input.BlueDown)
        {
            amountB -= colorEvolvSpeed * Time.deltaTime;
        }
        amountB = Mathf.Clamp01(amountB);

        UpdateColor();
    }

    private void ReturnToOriginal()
    {
        //Shape or Side
        if (!input.SideDown && !input.SideUp)
        {
            if (Mathf.Abs(baseSide - side) > 0.05f)
            {
                side = Mathf.Lerp(side, baseSide, returnBaseSideSpeed * Time.deltaTime);
            }
            else
            {
                side = baseSide;
            }
        }
        
        UpdateSide();

        //Color
        if (!input.RedUp && !input.RedDown)
        {
            if (Mathf.Abs(baseColor.r - amountR) > 0.05f)
            {
                amountR = Mathf.Lerp(amountR, baseColor.r, returnBaseColorSpeed * Time.deltaTime);
            }
            else
            {
                amountR = baseColor.r;
            }
        }
        if (!input.GreenUp && !input.GreenDown)
        {
            if (Mathf.Abs(baseColor.g - amountG) > 0.05f)
            {
                amountG = Mathf.Lerp(amountG, baseColor.g, returnBaseColorSpeed * Time.deltaTime);
            }
            else
            {
                amountG = baseColor.g;
            }
        }
        if (!input.BlueUp && !input.BlueDown)
        {
            if (Mathf.Abs(baseColor.b - amountB) > 0.05f)
            {
                amountB = Mathf.Lerp(amountB, baseColor.b, returnBaseColorSpeed * Time.deltaTime);
            }
            else
            {
                amountB = baseColor.b;
            }
        }

        UpdateColor();
    }

    private void OnDrawGizmos()
    {
        Orientation();
    }

    private void EditorOrientation()
    {
        transform.rotation = Quaternion.Euler(0, 0, baseAngle);
    }
}