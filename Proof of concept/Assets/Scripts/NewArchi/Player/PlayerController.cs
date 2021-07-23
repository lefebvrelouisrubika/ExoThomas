using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerController : Shape
{
    [Header("Components")]
    public InputHandler input;
    public Rigidbody2D rb;
    public static PlayerController instance;

    [Header("BaseState")]
    public float crackLvl = 0f;
    public float baseSide = 3;
    [ColorUsage(false, false)] public Color baseColor = new Color(1f, 0.5f, 0.75f);
    public float baseHue;

    [Header("Mouvement Parameter")]
    public float moveSpeed = 10f;
    public float turnSpeed = 10f;
    [Range(0,360)]
    public float baseAngle = 0f;
    public Vector2 orientation = Vector2.right;

    [Header("Evolving Parameter")]
    [Range(0.5f, 1f)] public float sideEvolvSpeed = 1f;
    [Range(0, 1f)] public float returnBaseSideSpeed = 1f;
    [Space(10)]
    [Range(0.0625f, 0.1875f)] public float hueEvolvSpeed = 1f;
    [Range(0, 0.125f)] public float returnBaseHueSpeed = 1f;

    [Header("sounds")]
    public AudioClip hit;
    public AudioClip changeColor;
    public AudioClip walk;

    public override void Awake()
    {
        base.Awake();

        instance = this;

        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody2D>();

        Color.RGBToHSV(baseColor, out baseHue, out _, out _);
    }

    private void Start()
    {
        crackLvl = 0f;
        mat.SetFloat("CrackLvl", crackLvl);
    }

    private void Update()
    {
        Movement();
        Orientation();

        Morphing();
        ChangeHue();

        ReturnToOriginal();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PNJ>())
        {
            if (collision.gameObject.GetComponent<PNJ>().isAttacking)
            {
                crackLvl += 0.1f;
                crackLvl = Mathf.Clamp01(crackLvl);
                mat.SetFloat("CrackLvl", crackLvl/2);
                Soundmanager.Instance.PlaySFX(hit, 0.5f);
            }
        }
    }

    private void Movement()
    {
        if (HueDistance(baseHue, hue) > 0.1f || SideDistance( baseSide,side) > 1.05f)
        {
            //Mouv
            //Debug.Log("ralenti");
            //Debug.Log(SideDistance(baseSide, side));
            
            rb.velocity = new Vector2(input.mouvHori, input.mouvVert).normalized * moveSpeed * 0.5f;
            //Soundmanager.Instance.PlayMusic(walk, 1f);
        }
        else
        {
            //Mouv
            rb.velocity = new Vector2(input.mouvHori, input.mouvVert).normalized * moveSpeed;
            //Soundmanager.Instance.PlayMusic(walk, 1f);
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
                
            }

        }
        //if(rb.velocity == new Vector2(0,0))
        //{
        //   //Soundmanager.Instance.stopMusic();
        //}
    }
    private void Orientation()
    {
        float angle = Mathf.Atan2(-input.mouvHori, input.mouvVert) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, baseAngle + angle), Time.deltaTime * turnSpeed);
    }

    private void Morphing()
    {
        if (true)
        {
            //Palier
            if (input.SideUp)
            {
                float nextSide = side + sideEvolvSpeed;
                nextSide = Mathf.Round(nextSide);
                side = Mathf.Clamp(nextSide, MinSide, MaxSide);
            }
            if (input.SideDown)
            {
                float nextSide = side - sideEvolvSpeed;
                nextSide = Mathf.Round(nextSide);
                side = Mathf.Clamp(nextSide, MinSide, MaxSide);
            }
        }
        else
        {
            if (input.SideUp)
            {
                float nextSide = side + sideEvolvSpeed;
                side = Mathf.Clamp(nextSide, MinSide, MaxSide);
            }
            if (input.SideDown)
            {
                float nextSide = side - sideEvolvSpeed;
                side = Mathf.Clamp(nextSide, MinSide, MaxSide);
            }
        }

        UpdateSide();
    }
    private void ChangeHue()
    {
        if (true)
        {
            if (input.HueUp)
            {
                hue += hueEvolvSpeed;

                hue %= 1;
                Soundmanager.Instance.PlaySFX(changeColor, 1f);
            }
            if (input.HueDown)
            {
                hue -= hueEvolvSpeed;

                hue = hue < 0 ? hue + 1 : hue;
                hue %= 1;
                Soundmanager.Instance.PlaySFX(changeColor, 1f);
            }
        }
        else
        {
            if (input.HueUp)
            {
                hue += hueEvolvSpeed;

                hue %= 1;
            }
            if (input.HueDown)
            {
                hue -= hueEvolvSpeed;

                hue = hue < 0 ? hue + 1 : hue;
                hue %= 1;
            }
        }

        hue = hue < 0 ? hue + 1 : hue;
        hue %= 1;

        UpdateColor();
        
    }

    private void ReturnToOriginal()
    {
        ToOriginalShape();
        ToOriginalHue();
    }
    private void ToOriginalShape()
    {
        //Shape or Side
        if (!input.SideDown && !input.SideUp)
        {
            if (Mathf.Abs(baseSide - side) > 0.05f)
            {
                if (baseSide < side)
                {
                    side -= returnBaseSideSpeed * Time.deltaTime;
                }
                else
                {
                    side += returnBaseSideSpeed * Time.deltaTime;
                }
            }
            else
            {
                side = baseSide;
            }
        }
        UpdateSide();
    }
    private void ToOriginalHue()
    {
        if (Mathf.Abs(baseHue - hue) < 0.5f)
        {
            if (hue < baseHue)
            {
                //Add
                hue += returnBaseHueSpeed * Time.deltaTime;
            }
            if (hue > baseHue)
            {
                //Substract
                hue -= returnBaseHueSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (baseHue > 0.5f)
            {
                hue -= returnBaseHueSpeed * Time.deltaTime;
            }
            else
            {
                hue += returnBaseHueSpeed * Time.deltaTime;
            }
            hue = hue < 0 ? hue + 1 : hue;
            hue %= 1;
        }

        UpdateColor();
    }

}