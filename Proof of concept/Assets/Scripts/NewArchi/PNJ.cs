using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCBehaviour
{
    Neutral,
    Happy,
    Attack,
    Flee,
    Block
};

[RequireComponent(typeof(SpriteRenderer))]
public class PNJ : Shape
{
    private PlayerController player;

    [Header("Comportement")]
    public NPCBehaviour stateSimilar = NPCBehaviour.Happy;
    public NPCBehaviour stateNeutral = NPCBehaviour.Neutral;
    public NPCBehaviour stateFaraway = NPCBehaviour.Attack;
    [Space(10)]
    [Range(0f, 1f)] public float similarthreshold;
    [Range(0f, 1f)] public float neutralthreshold;

    private float playerProximity;

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
        EvaluatePlayer();

    }

    private void EvaluatePlayer()
    {
        float sideProxi = 1 - SideDistance(side, player.side);
        float colorProxi = 1 - ColorDistance(color, player.color);

        playerProximity = (sideProxi + colorProxi)/2;
    }

    private void Neutral()
    {

    }
    private void Happy()
    {

    }
    private void Attack()
    {

    }
    private void Flee()
    {

    }
    private void Block()
    {

    }

}
