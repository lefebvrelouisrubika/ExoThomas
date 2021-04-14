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

    void Awake()
    {
        instance = this;
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shape();
        //Size();
        ChangeColor();

    }

    void Shape()
    {
        if (Input.GetButton("a"))
        {
            Sides = Mathf.Clamp(Sides + sizeAugment, 3, 16);
            rend.material.SetFloat("Sides", Sides);
        }
        if (Input.GetButton("r"))
        {
            Sides = Mathf.Clamp(Sides - sizeAugment, 3, 16);
            rend.material.SetFloat("Sides", Sides);
        }
    }
    private void Move()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        
        float inputVertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(inputHorizontal, inputVertical).normalized * (speed * speedModifier);

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
            RAmmount = Mathf.Clamp(RAmmount + colorAugment, 0, 1);
        }
        if (Input.GetButton("e"))
        {
            GAmmount = Mathf.Clamp(GAmmount + colorAugment, 0, 1);
        }
        if (Input.GetButton("q"))
        {
            BAmmount = Mathf.Clamp(BAmmount + colorAugment, 0, 1);
        }

        if (Input.GetButton("s"))
        {
            RAmmount = Mathf.Clamp(RAmmount - colorAugment, 0, 1);
        }
        if (Input.GetButton("d"))
        {
            GAmmount = Mathf.Clamp(GAmmount - colorAugment, 0, 1);
        }
        if (Input.GetButton("f"))
        {
            BAmmount = Mathf.Clamp(BAmmount- colorAugment, 0, 1);
        }
        rend.material.SetColor("_Color", new Vector4(RAmmount, GAmmount, BAmmount, 1));
    }
}
