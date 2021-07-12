using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.VFX;
using DG.Tweening;

public enum NPCBehaviour
{
    Neutral,
    Happy,
    Attack,
    Flee,
    Block,
    Routine
};

[RequireComponent(typeof(SpriteRenderer))]
public class PNJ : Shape
{
    private PlayerController player;
    private PNJGroup group;
    private NPCBehaviour actualBehaviour = NPCBehaviour.Happy;

    [Header("Comportement")]
    public NPCBehaviour stateSimilar = NPCBehaviour.Happy;
    public NPCBehaviour stateNeutral = NPCBehaviour.Neutral;
    public NPCBehaviour stateFaraway = NPCBehaviour.Attack;
    [Space(10)]
    [Range(0f, 1f)] public float similarthreshold;
    [Range(0f, 1f)] public float neutralthreshold;
    [HideInInspector] public float playerLookProximity;
    
    [Header("Player")]
    [HideInInspector] public bool playerInArea;
    private float playerDistance;

    [Header("Attack")]
    public bool isAttacking = false;
    public float attackSpeed = 10f;
    public float attackDistance = 1f;

    [Header("Blocking")]
    public Transform blockPos = null;
    public float moveSpeed = 10f;

    [Header("Flee")]
    public float fleeSpeed = 10f;

    [Header("Happy")]
    public ParticleSystem vfxHappy;

    [Header("Neutral")]
    public bool placeholder;

    void Start()
    {
        player = PlayerController.instance;

        float h;
        Color.RGBToHSV(color, out h, out _, out _);
        sprRend.color = Color.HSVToRGB(h, 0.36f, 1f);

        //Create a new instance of the material (use sharedMat for not changing it)
        rend.material.SetFloat("Sides", side);
    }

    void Update()
    {
        if (playerInArea)
        {
            //Calcul Distance
            playerDistance = (player.transform.position - transform.position).magnitude;
            
            if (!group.groupOverwrite)
            {
                EvaluatePlayer();
            }

            ChooseBehavior();
        }
        else
        {
            actualBehaviour = NPCBehaviour.Routine;
            PlayBehaviour();
        }
    }

    private void EvaluatePlayer()
    {
        float sideProxi = 1 - SideDistance(side, player.side);
        float colorProxi = 1 - ColorDistance(color, player.color);

        playerLookProximity = (sideProxi + colorProxi)/2;
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
        switch (actualBehaviour)
        {
            case NPCBehaviour.Neutral:

                break;

            case NPCBehaviour.Happy:

                break;

            case NPCBehaviour.Attack:

                break;

            case NPCBehaviour.Flee:

                break;

            case NPCBehaviour.Block:

                break;

            case NPCBehaviour.Routine:

                break;

            default:

                break;
        }
    }

    private void Neutral()
    {

    }

    private void Happy()
    {

    }

    private void Attack()
    {
        if (isAttacking)
        {

        }
        else if (player)
        { 
        
        }
    }

    private void Flee()
    {

    }

    private void Block()
    {

    }

    #region Debug
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, attackDistance);
        Handles.color = Color.white;
    }

#endif
    #endregion
}
