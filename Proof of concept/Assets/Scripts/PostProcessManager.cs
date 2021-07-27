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

    public int fleeScore;
    public int happyScore;
    public int attackScore;
    public int blockScore;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
       
    }
}
