using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Happy,
    Flee,
    Attack,
    Neutral,
}
public class NPC : MonoBehaviour
{


    public State state = State.Neutral;
    
    Renderer rend;

    public Player Player;
    public float Sides = 3;
    public float Width = 1;
    public float Height = 1;
    public float RAmmount;
    public float GAmmount;
    public float BAmmount;
    public float fleeDistance;
    public float fleeSpeed;
    public float attackDistanceMin, attackDistanceMax;
    private Player myPlayer;
    //public bool flee = false;
    //public bool attack = false;
    public float Tlerp;
    private bool canInitialiseAttack = true;
    private Vector3 initialPosition;
    private Vector3 attackDestination;
    private bool isWaitingForNextAttack = false;
    public float attackTLerp = 0.01f;
    private bool hasReachedAttackDestination;
    private bool hasFinishAttack = false;
    //private bool happy = false;
    public float rotLerp = 0.01f;
    private float rotationGoal;
    private bool isRotating = false;
    [SerializeField]
    private float palier1;
    [SerializeField]
    private float palier2;
    [SerializeField]
    private float palier3;



    // Start is called before the first frame update
    void Start()
    {
        myPlayer = Player.instance;
        rend = GetComponent<Renderer>();
        rend.material.SetFloat("Sides", Sides);
        rend.material.SetColor("_Color", new Vector4(RAmmount, GAmmount, BAmmount, 1));
    }

    // Update is called once per frame
    void Update()
    {

        Behaviour();
        StateCalcul();


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    happy = true;
        //}
        //if (happy)
        //{
        //    if (!isRotating)
        //    {
        //        while (Mathf.Abs(this.transform.eulerAngles.z - rotationGoal)%360 < 45) { rotationGoal = Random.Range(0f, 360f); }

        //        isRotating = true;
        //    }
        //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, rotationGoal), rotLerp);
        //    isRotating = Vector3.Distance(this.transform.eulerAngles, new Vector3(0, 0, rotationGoal)) > 0.1f;
        //}
        //if (attack)
        //{
        //    if (Vector2.Distance(myPlayer.transform.position, this.transform.position) < attackDistanceMax & !isWaitingForNextAttack)
        //    {
        //        if (canInitialiseAttack)
        //        {
        //            initialPosition = this.transform.position;
        //            attackDestination = initialPosition + (myPlayer.transform.position - initialPosition) / 2;
        //            canInitialiseAttack = false;
        //            hasFinishAttack = false;
        //            hasReachedAttackDestination = false;
        //        }
        //        else
        //        {
        //            if (hasFinishAttack)
        //            {
        //                StartCoroutine("WaitForNextAttack");
        //            }
        //            if (!hasReachedAttackDestination)
        //            {
        //                this.transform.position = Vector3.Lerp(this.transform.position, attackDestination, attackTLerp);
        //                hasReachedAttackDestination = Vector3.Distance(this.transform.position, attackDestination) < 0.1f;
        //            }
        //            else
        //            {
        //                this.transform.position = Vector3.Lerp(this.transform.position, initialPosition, attackTLerp);
        //                hasFinishAttack = Vector3.Distance(this.transform.position, initialPosition) < 0.1f;
        //            }
        //        }
        //    }


        //}
        //if (flee)
        //{
        //    if (Vector2.Distance(myPlayer.transform.position, this.transform.position) < fleeDistance)
        //    {
        //        this.transform.position = Vector2.Lerp(this.transform.position, this.transform.position + ((this.transform.position - myPlayer.transform.position).normalized * fleeSpeed), Tlerp);
        //    }

        //}

    }

    void Behaviour()
    {
        switch (state)
        {
            case State.Neutral:

                break;
            case State.Flee:
                if (Vector2.Distance(myPlayer.transform.position, this.transform.position) < fleeDistance)
                {
                    this.transform.position = Vector2.Lerp(this.transform.position, this.transform.position + ((this.transform.position - myPlayer.transform.position).normalized * fleeSpeed), Tlerp);
                }
                break;
            case State.Attack:
                if (Vector2.Distance(myPlayer.transform.position, this.transform.position) < attackDistanceMax & !isWaitingForNextAttack)
                {
                    if (canInitialiseAttack)
                    {
                        initialPosition = this.transform.position;
                        attackDestination = initialPosition + (myPlayer.transform.position - initialPosition) / 2;
                        canInitialiseAttack = false;
                        hasFinishAttack = false;
                        hasReachedAttackDestination = false;
                    }
                    else
                    {
                        if (hasFinishAttack)
                        {
                            StartCoroutine("WaitForNextAttack");
                        }
                        if (!hasReachedAttackDestination)
                        {
                            this.transform.position = Vector3.Lerp(this.transform.position, attackDestination, attackTLerp);
                            hasReachedAttackDestination = Vector3.Distance(this.transform.position, attackDestination) < 0.1f;
                        }
                        else
                        {
                            this.transform.position = Vector3.Lerp(this.transform.position, initialPosition, attackTLerp);
                            hasFinishAttack = Vector3.Distance(this.transform.position, initialPosition) < 0.1f;
                        }
                    }
                }
                break;
            case State.Happy:
                if (!isRotating)
                {
                    while (Mathf.Abs(this.transform.eulerAngles.z - rotationGoal) % 360 < 45) { rotationGoal = Random.Range(0f, 360f); }

                    isRotating = true;
                }
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, rotationGoal), rotLerp);
                isRotating = Vector3.Distance(this.transform.eulerAngles, new Vector3(0, 0, rotationGoal)) > 0.1f;
                break;
            default:

                break;
        }
    }
    void StateCalcul()
    {
        float calculSides = Mathf.Abs(Sides-Player.Sides)+1;
        var s = 1 / calculSides;
        var r = 1 - Mathf.Abs(RAmmount - Player.RAmmount);
        var b = 1 - Mathf.Abs(BAmmount - Player.BAmmount);
        var g = 1 - Mathf.Abs(GAmmount - Player.GAmmount);

        var moyenne = (s + r + b + g) / 4;
        if (moyenne>= palier1)
        {
            state = State.Happy;

        }
        else if (moyenne >= palier2)
        {
            state = State.Neutral;

        }
        else if (moyenne >= palier3)
        {
            state = State.Flee;

        }
        else if (moyenne < palier3)
        {
            state = State.Attack;

        }
        Debug.Log(moyenne);
        AmbientManager.instance.state = state;
    }

    IEnumerator WaitForNextAttack()
    {
        isWaitingForNextAttack = true;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        canInitialiseAttack = true;
        isWaitingForNextAttack = false;
    }
    //void Dash()
    //{
    //    canStartDash = false;
    //    Debug.Log("Dash");
    //    Vector2 dashPosition = initialDashPosition - ((initialDashPosition - new Vector2(myPlayer.transform.position.x, myPlayer.transform.position.y)).normalized * dashRange);
    //    this.transform.position = Vector2.Lerp(this.transform.position, dashPosition, dashTlerp);
    //    //yield return new WaitForSeconds(1);
    //    if (Vector2.Distance(this.transform.position, dashPosition) < 0.1f) isDashing = false ;
    //}
    //void Dashback()
    //{
    //    canStartDashBack = false;
    //    Debug.Log("DashBack");
    //    this.transform.position = Vector2.Lerp(this.transform.position, initialDashPosition, dashTlerp);
    //    //yield return new WaitForSeconds(Random.Range(1, 5));
    //    if(Vector2.Distance(this.transform.position,initialDashPosition)<0.1f) canStartDash = true;
    //}


}
