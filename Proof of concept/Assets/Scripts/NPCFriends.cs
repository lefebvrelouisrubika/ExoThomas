using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFriends : MonoBehaviour
{


    public State state = State.Neutral;

    Renderer rend;

    public ParticleSystem HappyParticles;
    public float Sides = 3;
    public float Width = 1;
    public float Height = 1;
    public float RAmmount;
    public float GAmmount;
    public float BAmmount;
    public float happyDistance;
    private PlayerController myPlayer;
    //public bool flee = false;
    //public bool attack = false;
    public float rotLerp = 0.01f;
    private float rotationGoal;
    private bool isRotating = false;
    [SerializeField]
    private float rotSpeed = 1;
    private bool waitForRotate = false;




    // Start is called before the first frame update
    void Start()
    {
        myPlayer = PlayerController.instance;
        rend = GetComponent<Renderer>();
        rend.material.SetFloat("Sides", Sides);
        rend.material.SetColor("_Color", new Vector4(RAmmount, GAmmount, BAmmount, 1));
    }

    // Update is called once per frame
    void Update()
    {

        Behaviour();


    }

    void Behaviour()
    {
        if(Vector2.Distance(myPlayer.transform.position, this.transform.position) < happyDistance )
        {

                if (!isRotating)
                {

                    while (Mathf.Abs(this.transform.eulerAngles.z - rotationGoal) % 360 < 45)
                    {
                        rotationGoal = Random.Range(0f, 360f);
                    }
                    if (!waitForRotate)
                    {
                        StartCoroutine("WaitForRotate");
                    }

                }
                else
                {

                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, rotationGoal), rotLerp * Time.deltaTime * rotSpeed);
                    isRotating = Vector3.Distance(this.transform.eulerAngles, new Vector3(0, 0, rotationGoal)) > 0.1f;
                    waitForRotate = false;
                }

        }

    }
    IEnumerator WaitForRotate()
    {
        waitForRotate = true;
        HappyParticles.Play();
        yield return new WaitForSeconds(0.6f);
        isRotating = true;

    }




}
