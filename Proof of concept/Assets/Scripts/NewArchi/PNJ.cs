using System.Collections;
using UnityEngine;
using UnityEditor;

public enum NPCBehaviour
{
    Neutral,
    Happy,
    Attack,
    Flee,
    Block,
    Routine,
    AFK
};

[RequireComponent(typeof(SpriteRenderer))]
public class PNJ : Shape
{
    public PlayerController player;
    public PNJGroup group;
    private Rigidbody2D rb;

    private NPCBehaviour actualBehaviour = NPCBehaviour.Happy;

    [Header("Comportement")]
    public NPCBehaviour stateSimilar = NPCBehaviour.Happy;
    public NPCBehaviour stateNeutral = NPCBehaviour.Neutral;
    public NPCBehaviour stateFaraway = NPCBehaviour.Attack;
    [Space(10)]
    [Range(0f, 1f)] public float similarthreshold;
    [Range(0f, 1f)] public float neutralthreshold;
    public float playerLookProximity;
    
    [Header("Player")]
    [HideInInspector] public bool playerInArea;
    private Vector2 toPlayerVector;
    private float playerDistance;

    [Header("EnnemisData")]
    public Transform defaultPosition = null;

    [Header("Sounds")]
    public AudioClip fuite;
    public float screamVolume;
    private Vector3 defaultPos
    {
        get 
        {
            if (defaultPosition != null)
            {
                return defaultPosition.position;
            }
            else
            {
                GameObject go = new GameObject(this.gameObject.name + "_DefaultPos");
                go.transform.SetParent(this.transform.parent);
                defaultPosition = go.transform;
                defaultPosition.position = transform.position;

                return defaultPosition.position; 
            }
        }
        set { 
            defaultPosition.position = value; 
        }
    }

    [Header("Attack")]
    public bool isAttacking = false;
    public float attackSpeed = 0.1f;
    public float attackDistance = 1f;

    [Header("Blocking")]
    public Transform blockPos = null;
    public float moveSpeed = 10f;

    [Header("Flee")]
    public ParticleSystem vfxFlee;
    public float fleeSpeed = 10f;
    public float fleeDistance = 2f;
    public bool disapear;
    private bool noTurningBack = false;
    public float DisapearRatio;
    private bool hasScreamed = false;

    [Header("Happy")]
    public ParticleSystem vfxHappy;
    public float rotSpeed = 10f;
    public bool isRotating;

    [Header("Neutral")]
    public bool placeholder;

    void Start()
    {
        player = PlayerController.instance;
        rb = GetComponent<Rigidbody2D>();
        float h;
        Color.RGBToHSV(color, out h, out _, out _);
        sprRend.color = Color.HSVToRGB(h, 0.36f, 1f);

        //Create a new instance of the material (use sharedMat for not changing it)
        rend.material.SetFloat("Sides", side);
    }

    void Update()
    {
        //Calcul Distance
        toPlayerVector = player.transform.position - transform.position;
        playerDistance = toPlayerVector.magnitude;

        if (playerInArea)
        {
            if (!group.groupOverwrite)
            {
                EvaluatePlayer();
            }

            ChooseBehavior();
        }
        else
        {
            actualBehaviour = NPCBehaviour.Routine;
        }
        
        PlayBehaviour();
    }

    private void EvaluatePlayer()
    {
        float sideProxi = Shape.SideDistance(side, player.side);
        //compare distance (1- / oneMinus to convert distance in proximity)
        float colorProxi = 1 - Shape.HueDistance(hue, player.hue);

        playerLookProximity = (sideProxi + colorProxi) / 2;
    }

    private void ChooseBehavior()
    {
        if (playerLookProximity < neutralthreshold)
        {
            actualBehaviour = stateFaraway;
        }
        else
        if (playerLookProximity < similarthreshold)
        {
            actualBehaviour = stateNeutral;
        }
        else
        {
            actualBehaviour = stateSimilar;
        }
    }

    private void PlayBehaviour()
    {
        if (!isAttacking)
        {
            switch (actualBehaviour)
            {
                case NPCBehaviour.Neutral:
                    Neutral();
                    break;

                case NPCBehaviour.Happy:
                    Happy();
                    break;

                case NPCBehaviour.Attack:
                    Attack();
                    break;

                case NPCBehaviour.Flee:
                    Flee();
                    break;

                case NPCBehaviour.Block:
                    Block();
                    break;

                case NPCBehaviour.Routine:
                    Routine();
                    break;

                case NPCBehaviour.AFK:
                    //Nothing
                    break;

                default:
                    Routine();
                    break;
            }
        }
    }

    private void Neutral()
    {
        Vector2 toDefaultPos = defaultPos - transform.position;

        if (toDefaultPos.magnitude > 0.1f)
        {
            transform.Translate(toDefaultPos.normalized * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void Happy()
    {
        Vector2 toDefaultPos = defaultPos - transform.position;

        if (toDefaultPos.magnitude > 0.1f)
        {
            transform.Translate(toDefaultPos.normalized * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            if (!vfxHappy.isPlaying)
            {
                vfxHappy.Play();
            }
            if (vfxHappy.time > vfxHappy.main.duration)
            {
                vfxHappy.Stop();
            }

            if (!isRotating)
            {
                isRotating = true;
                StartCoroutine(RotateCorout());
            }
        }
    }

    private void Attack()
    {
        if (toPlayerVector.magnitude < attackDistance)
        {
            if (!isAttacking)
            {
                isAttacking = true;

                StartCoroutine(AttackCorout(player.transform.position));
            }
        }
    }

    private IEnumerator AttackCorout(Vector3 targetAttackPos)
    {
        isAttacking = true;

        //Attack
        Vector2 toAttackPos = targetAttackPos - transform.position;
        while (toAttackPos.magnitude > 0.1f)
        {
            transform.position = Vector3.Lerp(this.transform.position, targetAttackPos, attackSpeed * Time.deltaTime);
            //transform.Translate(toAttackPos.normalized * attackSpeed * Time.deltaTime, Space.World);

            toAttackPos = targetAttackPos - transform.position;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(Random.Range(0.3f, 0.5f));

        if (actualBehaviour != NPCBehaviour.Attack)
        {
            isAttacking = false;
            yield break;
        }

        //Retrive
        Vector2 toDefaultPos = defaultPos - transform.position;
        while (toDefaultPos.magnitude > 0.1f)
        {
            //transform.position = Vector3.Lerp(this.transform.position, toDefaultPos, moveSpeed * Time.deltaTime);
            transform.Translate(toDefaultPos.normalized * moveSpeed * Time.deltaTime, Space.World);

            toDefaultPos = defaultPos - transform.position;
            yield return new WaitForEndOfFrame();
        }

        if (actualBehaviour != NPCBehaviour.Attack)
        {
            isAttacking = false;
            yield break;
        }

        //Wait for next attack
        yield return new WaitForSeconds(Random.Range(0.3f, 0.5f));
        isAttacking = false;
    }
    private IEnumerator RotateCorout()
    {
        isRotating = true;
        float rotGoal = transform.rotation.z;
        bool clockwize = false;
        //Rotate
        while (actualBehaviour == NPCBehaviour.Happy)
        {
            //Reset Rotation
            rotGoal += clockwize ? Random.Range(15, 360) : -Random.Range(15, 360);
            if (rotGoal < 0)
            {
                rotGoal += 360;
            }
            rotGoal %= 360;

            clockwize = !clockwize;

            //Rotate
            while (Mathf.Abs(rotGoal - transform.rotation.eulerAngles.z) > 3f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotGoal), rotSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSecondsRealtime(0.5f);
        }

        isRotating = false;
    }

    private void Flee()
    {

        if (toPlayerVector.magnitude < fleeDistance || noTurningBack == true)
        {
            //rb.velocity = -toPlayerVector.normalized * fleeSpeed * Time.deltaTime;
            transform.Translate(-toPlayerVector.normalized * fleeSpeed * Time.deltaTime, Space.World);
            
            if (!vfxFlee.isPlaying)
            {
                vfxFlee.Play(); 
            }

            if (hasScreamed == false)
            {
                Debug.Log("AH!");
                hasScreamed = true;
                Soundmanager.Instance.PlaySFX(fuite, screamVolume);
            }

            if (disapear == true)
            {
                noTurningBack = true;
                disapear = false;
                StartCoroutine(FadeOut());
            }
        }
        else
        {
            vfxFlee.Stop();
        }
    }

    private void Block()
    {
        Vector2 toBlockPos =  blockPos.position - transform.position;

        if (toBlockPos.magnitude > 0.1f)
        {
            transform.Translate(toBlockPos.normalized * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private void Routine()
    {
        Vector2 toDefaultPos = defaultPos - transform.position;

        if (toDefaultPos.magnitude > 0.1f)
        {
            transform.Translate(toDefaultPos.normalized * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.5f);

        Color spriteColor = this.GetComponent<SpriteRenderer>().color;

        while (this.GetComponent<SpriteRenderer>().color.a > 0)
        {
            Debug.Log("Sayonara");
            spriteColor.a -= DisapearRatio;

            this.GetComponent<SpriteRenderer>().color = spriteColor;

            yield return new WaitForSeconds(0.05f);
        }

        this.gameObject.SetActive(false);
    }

    #region Debug
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, attackDistance);

        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.forward, fleeDistance);

        Debug.DrawLine(transform.position, defaultPos, Color.black);

        Handles.color = Color.black;
        Debug.DrawLine(transform.position, blockPos.position, Color.black);
        float scale = 0.5f;
        Handles.DrawWireDisc(blockPos.position, Vector3.forward, scale);
        Handles.DrawWireDisc(blockPos.position, Vector3.right, scale);
        Handles.DrawWireDisc(blockPos.position, Vector3.up, scale);

        Handles.color = Color.white;
    }

#endif
    #endregion
}
