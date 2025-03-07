using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public static Player instance;
    Renderer rend;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float speedModifier;
    public float Sides = 3;
    public float Width = 1;
    public float Height = 1;
    public float RAmmount ;
    public float GAmmount ;
    public float BAmmount ;
    public float colorAugment;
    public float sizeAugment;
    private Rigidbody2D rb;
    private float originalSides;
    private float originalRAmmount;
    private float originalGAmmount;
    private float originalBAmmount;
    public float returnModifier;
    public float Tlerp;
    public float returnSpeed =150 ;
    public float returnSpeedSides = 150;
    public float turnSpeed = 10;

    void Awake()
    {
        instance = this;
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
        originalSides=Sides;
        originalRAmmount=RAmmount;
        originalGAmmount=GAmmount;
        originalBAmmount=BAmmount;
}

    void Update()
    {
        Move();
        Shape();
        //Size();
        ChangeColor();
        ReturnToOriginal();

    }

    void Shape()
    {
        if (Input.GetButton("a"))
        {
            Sides = Mathf.Clamp(Sides + (sizeAugment * Time.deltaTime), 3, 10);
            rend.material.SetFloat("Sides", Sides);
        }
        if (Input.GetButton("r"))
        {
            Sides = Mathf.Clamp(Sides - (sizeAugment * Time.deltaTime), 3, 10);
            rend.material.SetFloat("Sides", Sides);
        }
    }

    void ReturnToOriginal()
    {
        if(originalSides!=Sides || originalRAmmount!=RAmmount || originalGAmmount!=GAmmount || originalBAmmount!=BAmmount)
        {
            if (Sides > originalSides)
            {
                Sides = Mathf.Lerp(Sides, originalSides, Tlerp * Time.deltaTime * returnSpeedSides); 
                rend.material.SetFloat("Sides", Sides);
            }

            RAmmount = Mathf.Lerp(RAmmount, originalRAmmount, Tlerp * Time.deltaTime * returnSpeed);
            GAmmount = Mathf.Lerp(GAmmount, originalGAmmount, Tlerp * Time.deltaTime * returnSpeed);
            BAmmount = Mathf.Lerp(BAmmount, originalBAmmount, Tlerp * Time.deltaTime * returnSpeed);
            //if (RAmmount > originalRAmmount)
            //{
            //    RAmmount = Mathf.Clamp(RAmmount - returnModifier * (RAmmount / originalRAmmount),originalRAmmount,0);
            //}
            //if (RAmmount < originalRAmmount)
            //{
            //    RAmmount = Mathf.Clamp(RAmmount + returnModifier * (RAmmount / originalRAmmount),0,originalRAmmount);
            //}
            //if (GAmmount > originalGAmmount)
            //{
            //    GAmmount = Mathf.Clamp(GAmmount - returnModifier * (GAmmount / originalGAmmount),originalGAmmount,0);
            //}
            //if (GAmmount < originalGAmmount)
            //{
            //    GAmmount = Mathf.Clamp(GAmmount + returnModifier * (GAmmount / originalGAmmount),0,originalGAmmount);
            //}
            //if (BAmmount > originalBAmmount)
            //{
            //    BAmmount = Mathf.Clamp(BAmmount - returnModifier * (BAmmount / originalBAmmount),originalBAmmount,1);
            //}
            //if (BAmmount < originalBAmmount)
            //{
            //    BAmmount = Mathf.Clamp(BAmmount + returnModifier * (BAmmount / originalBAmmount),0,originalBAmmount);
            //}
            rend.material.SetColor("_Color", new Vector4(RAmmount, GAmmount, BAmmount, 1));

            
        }
    }
    private void Move()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        
        float inputVertical = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(inputHorizontal, inputVertical)* (speed * speedModifier);
        float angle = Mathf.Atan2(-inputHorizontal, inputVertical) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime*turnSpeed);

    }
        //void Size()
        //{
        //    if (Input.GetButton("z"))
        //    {
        //        Height = Mathf.Clamp(Height + 0.001f, 0.5f, 1.1f);
        //        rend.material.SetFloat("Height", Height);
        //    }
        //    if (Input.GetButton("e"))
        //    {
        //        Width = Mathf.Clamp(Width + 0.001f, 0.5f, 1.1f);
        //        rend.material.SetFloat("Width", Width);
        //    }
        //    if (Input.GetButton("s"))
        //    {
        //        Height = Mathf.Clamp(Height - 0.001f, 0.5f, 1.1f);
        //        rend.material.SetFloat("Height", Height);
        //    }
        //    if (Input.GetButton("q"))
        //    {
        //        Width = Mathf.Clamp(Width - 0.001f, 0.5f, 1.1f);
        //        rend.material.SetFloat("Width", Width);
        //    }
        //}

    void ChangeColor()
    {
        if (Input.GetButton("z"))
        {
            RAmmount += colorAugment * Time.deltaTime;
            RAmmount = Mathf.Clamp01(RAmmount);

        }
        if (Input.GetButton("e"))
        {
            GAmmount += colorAugment * Time.deltaTime;
            GAmmount = Mathf.Clamp01(GAmmount);
        }
        if (Input.GetButton("q"))
        {
            BAmmount += colorAugment * Time.deltaTime;
            BAmmount = Mathf.Clamp01(BAmmount);
        }

        if (Input.GetButton("s"))
        {
            RAmmount -= colorAugment * Time.deltaTime;
            RAmmount = Mathf.Clamp01(RAmmount);
        }
        if (Input.GetButton("d"))
        {
            GAmmount -= colorAugment * Time.deltaTime;
            GAmmount = Mathf.Clamp01(GAmmount);
        }
        if (Input.GetButton("f"))
        {
            BAmmount -= colorAugment * Time.deltaTime;
            BAmmount = Mathf.Clamp01(BAmmount);
        }
        rend.material.SetColor("_Color", new Vector4(RAmmount, GAmmount, BAmmount, 1));
    }
}
