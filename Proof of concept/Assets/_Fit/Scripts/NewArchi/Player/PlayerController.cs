using UnityEngine;
using System.Collections;

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
    public float baseMoveSpeed = 10f;
     float moveSpeed = 10f;
    public float turnSpeed = 10f;
    public float woundedNerf = 1f;
    [Range(0,360)]
    public float baseAngle = 0f;
    public Vector2 orientation = Vector2.right;

    [Header("Evolving Parameter")]
    [Range(0.5f, 1f)] public float sideEvolvSpeed = 1f;
    [Range(0, 1f)] public float returnBaseSideSpeed = 1f;
    [Space(10)]
    [Range(0.0625f, 0.1875f)] public float hueEvolvSpeed = 1f;
    [Range(0, 0.125f)] public float returnBaseHueSpeed = 1f;
    public int returnShapeCooldown;
    private int currentShapeCD;


    private bool coroutineDownShape = false;
    public int returnColorCooldown;
    private int currentColorCD;
    private bool coroutineDownColor = false;
    private bool shaking = false;
    private bool colorTwitching = false;
    private float shakeHue;
    private bool isWalking = false;
    private bool ishealing = false;
    private bool healable = false;

    public float zoomAmount;
    float baseOrtho;

    

    [Header("sounds")]
    public AudioClip hit;
    public AudioClip changeColor;
    public AudioClip changeForm;
    public bool isInScene = false;

    //public AudioClip walk;

    public override void Awake()
    {
        base.Awake();
        baseOrtho = Camera.main.orthographicSize;
        instance = this;

        input = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody2D>();

        Color.RGBToHSV(baseColor, out baseHue, out _, out _);
        moveSpeed = baseMoveSpeed;
    }

    private void Start()
    {
        crackLvl = 0f;
        mat.SetFloat("CrackLvl", crackLvl);
        currentShapeCD = returnShapeCooldown;
        currentColorCD = returnColorCooldown;

        

    }

    private void Update()
    {
        Movement();
        Orientation();

        Morphing();
        ChangeHue();

        ReturnToOriginal();
        Shaking();
        ColorTwitching();
        //Debug.Log(shaking);
        ReturnCracks();

        if (Mathf.Abs(baseSide - side) > 0.05f)
        {
            //dissonance
            Soundmanager.Instance.ChangeVolume3(0.5f);
            Soundmanager.Instance.ChangeVolume2(0.001f);
        }
        if (Mathf.Abs(baseSide - side) < 0.05f)
        {
            //normal
            Soundmanager.Instance.ChangeVolume2(0.5f);
            Soundmanager.Instance.ChangeVolume3(0.001f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PNJ>())
        {
            if (collision.gameObject.GetComponent<PNJ>().isAttacking)
            {
                PostProcessManager.Instance.attackScore = PostProcessManager.Instance.attackScore+0.2f;
                crackLvl += 0.1f;
                crackLvl = Mathf.Clamp01(crackLvl);
                mat.SetFloat("CrackLvl", crackLvl/2);
                Soundmanager.Instance.PlaySFX(hit, 0.5f);
                healable = false;
                StartCoroutine("Wounded");
            }
        }
    }

    private void Movement()
    {
       
        if (HueDistance(baseHue, hue) > 0.1f || SideDistance( baseSide,side) < 1f)
        {
            //Mouv
            //Debug.Log("ralenti");
            //Debug.Log(SideDistance(baseSide, side));
            
            rb.linearVelocity = new Vector2(input.mouvHori, input.mouvVert).normalized * moveSpeed * 0.5f;
            if (isWalking == false)
            {
                //Debug.Log("son");
                //Soundmanager.Instance.PlayMusic(walk, 0.5f);
                isWalking = true;
            }

        }
        else
        {
            //Mouv
            rb.linearVelocity = new Vector2(input.mouvHori, input.mouvVert).normalized * moveSpeed;
            if (isWalking == false)
            {
                //Debug.Log("son");
                //Soundmanager.Instance.PlayMusic(walk, 0.5f);
                isWalking = true;
            }
            if (rb.linearVelocity.magnitude > moveSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
                
            }

        }
        if(rb.linearVelocity == new Vector2(0,0))
        {
            isWalking = false;
            //Soundmanager.Instance.StopMusic();
        }
    }
    private void Orientation()
    {
        Vector2 stickMouv = new Vector2(input.mouvHori, input.mouvVert);

        if(stickMouv.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(-input.mouvHori, input.mouvVert) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, baseAngle + angle), Time.deltaTime * turnSpeed);
        }
    }

    private void Morphing()
    {
        if (true)
        {
            //Palier
            if (shaking == false)
            {
                if (input.SideUp)
                {
                    float nextSide = side + sideEvolvSpeed;
                    nextSide = Mathf.Round(nextSide);
                    side = Mathf.Clamp(nextSide, MinSide, MaxSide);
                    currentShapeCD = returnShapeCooldown;
                    Soundmanager.Instance.PlaySFX(changeForm, 1f);
                }
                if (input.SideDown)
                {
                    float nextSide = side - sideEvolvSpeed;
                    nextSide = Mathf.Round(nextSide);
                    side = Mathf.Clamp(nextSide, MinSide, MaxSide);
                    currentShapeCD = returnShapeCooldown;
                    Soundmanager.Instance.PlaySFX(changeForm, 1f);
                }
            }
            else
            {
                if (input.SideDown)
                {
                    currentShapeCD = Random.Range(2, 4);
                }
                if (input.SideUp)
                {
                    currentShapeCD = Random.Range(2, 4);
                }

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
            if (colorTwitching == false)
            {
                if (input.HueUp)
                {
                    hue += hueEvolvSpeed;

                    hue %= 1;
                    Soundmanager.Instance.PlaySFX(changeColor, 1f);
                    currentColorCD = returnColorCooldown;
                }
                if (input.HueDown)
                {
                    hue -= hueEvolvSpeed;

                    hue = hue < 0 ? hue + 1 : hue;
                    hue %= 1;
                    Soundmanager.Instance.PlaySFX(changeColor, 1f);
                    currentColorCD = returnColorCooldown;
                }
            }
            else
            {
                if (input.HueUp)
                {
                    Soundmanager.Instance.PlaySFX(changeColor, 1f);
                    currentColorCD = Random.Range(2, 4);
                }
                if (input.HueDown)
                {
                    Soundmanager.Instance.PlaySFX(changeColor, 1f);
                    currentColorCD = Random.Range(2, 4);
                }
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

        if (Mathf.Abs(baseSide - side) > 0.05f)
        {
            if (currentShapeCD > 0)
            {


                if (currentShapeCD <= 1)
                {
                    //Debug.Log("test");

                    shaking = true;
                }
                if (currentShapeCD > 1)
                {

                    shaking = false;
                }
                if (coroutineDownShape == false)
                {
                    StartCoroutine("downshapeCD");
                }
            }
            else
            {
                side = baseSide;

                shaking = false;
            }
        }

        UpdateSide();
    }

    IEnumerator downshapeCD()
    {
        //Debug.Log("coroutine");
        coroutineDownShape = true;
        yield return new WaitForSeconds(1);
        currentShapeCD -=1;
        //Debug.Log(currentShapeCD);
        coroutineDownShape = false;
    }
    private void ToOriginalHue()
    {
        if (Mathf.Abs(baseHue - hue) > 0.05f)
        {
            //Debug.Log("test");
            if (currentColorCD > 0)
            {


                if (currentColorCD <= 1)
                {
                    

                    colorTwitching = true;
                }
                if (currentColorCD > 1)
                {
                    shakeHue = hue;
                    colorTwitching = false;
                }
                if (coroutineDownColor == false)
                {
                    StartCoroutine("downColorCD");
                }
            }
            else
            {
                hue = baseHue;

                colorTwitching = false;
            }
        }
        //if (baseHue > 0.5f)
        //{
        //    hue -= returnBaseHueSpeed * Time.deltaTime;
        //}
        //else
        //{
        //    hue += returnBaseHueSpeed * Time.deltaTime;
        //}
        //hue = hue < 0 ? hue + 1 : hue;
        //hue %= 1;
        //}

        UpdateColor();
    }
    IEnumerator downColorCD()
    {
        //Debug.Log("colorroutine");
        coroutineDownColor = true;
        yield return new WaitForSeconds(1);
        currentColorCD = currentColorCD - 1;
        coroutineDownColor = false;
    }
    private void Shaking()
    {

        if(shaking == true)
        {
            if (side >= 6)
            {
                mat.SetFloat("Sides", side + Random.Range(-0.6f, 0.6f));
            }
            else
            {
                mat.SetFloat("Sides", side + Random.Range(-0.3f, 0.3f));
            }
                
                //StartCoroutine("ShapeShake");

        }
        
    }
    private void ColorTwitching()
    {

        if (colorTwitching == true)
        {
            
            //Debug.Log("test");
            hue = shakeHue + Random.Range(-0.01f, 0.01f);
            //StartCoroutine("ShapeShake");
            

        }

    }
    private void ReturnCracks()
    {
        if (healable == true)
        {
            if (ishealing == false)
            {
                StartCoroutine("HealTime");
            }
        }

    }
    IEnumerator HealTime()
    {
        ishealing = true;
        yield return new WaitForSeconds(1);
        crackLvl -= 0.05f;
        crackLvl = Mathf.Clamp01(crackLvl);
        mat.SetFloat("CrackLvl", crackLvl / 2);
        ishealing = false;
    }
    IEnumerator Wounded()
    {
        CameraManager.Instance.targetOrtho -= zoomAmount;
        moveSpeed = Mathf.Clamp(moveSpeed - woundedNerf, 3, 10);
        yield return new WaitForSeconds(3);
        moveSpeed = baseMoveSpeed;
        healable = true;
        CameraManager.Instance.targetOrtho = baseOrtho;
    }


}