using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoBehaviour
{

    public static PostProcessManager Instance;


    public Volume FleeVolume;
    public Volume HappyVolume;
    public Volume AttackVolume;
    public Volume BlockVolume;

    public float fleeScore;
    public float happyScore;
    public float attackScore;
    public float blockScore;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        PostProcessUpdate();
    }

    private void PostProcessUpdate()
    {
        FleeVolume.weight = fleeScore;
        HappyVolume.weight = happyScore;
        AttackVolume.weight = attackScore;
        BlockVolume.weight = blockScore;
    }

}
