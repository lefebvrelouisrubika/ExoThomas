using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessHandler : MonoBehaviour
{
    public static PostProcessHandler instance;

    [Header("Post Process Volume")]
    public Volume volumeHappy;
    public Volume volumeAngry;
    public Volume volumeFlee;
    public Volume volumeBlock;

    private NPCBehaviour behaviorPP = NPCBehaviour.Neutral;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        switch (behaviorPP)
        {
            case NPCBehaviour.Happy:
                volumeHappy.weight = Mathf.Clamp01(volumeHappy.weight + Time.deltaTime);

                volumeAngry.weight = Mathf.Clamp01(volumeAngry.weight - Time.deltaTime);
                volumeFlee.weight = Mathf.Clamp01(volumeFlee.weight - Time.deltaTime);
                volumeBlock.weight = Mathf.Clamp01(volumeBlock.weight - Time.deltaTime);
                break;

            case NPCBehaviour.Attack:
                volumeAngry.weight = Mathf.Clamp01(volumeAngry.weight + Time.deltaTime);

                volumeHappy.weight = Mathf.Clamp01(volumeHappy.weight - Time.deltaTime);
                volumeFlee.weight = Mathf.Clamp01(volumeFlee.weight - Time.deltaTime);
                volumeBlock.weight = Mathf.Clamp01(volumeBlock.weight - Time.deltaTime);
                break;

            case NPCBehaviour.Flee:
                volumeFlee.weight = Mathf.Clamp01(volumeFlee.weight + Time.deltaTime);

                volumeHappy.weight = Mathf.Clamp01(volumeHappy.weight - Time.deltaTime);
                volumeAngry.weight = Mathf.Clamp01(volumeAngry.weight - Time.deltaTime);
                volumeBlock.weight = Mathf.Clamp01(volumeBlock.weight - Time.deltaTime);
                break;

            case NPCBehaviour.Block:
                volumeBlock.weight = Mathf.Clamp01(volumeBlock.weight + Time.deltaTime);

                volumeHappy.weight = Mathf.Clamp01(volumeHappy.weight - Time.deltaTime);
                volumeAngry.weight = Mathf.Clamp01(volumeAngry.weight - Time.deltaTime);
                volumeFlee.weight = Mathf.Clamp01(volumeFlee.weight - Time.deltaTime);
                break;

            case NPCBehaviour.Neutral:
                volumeHappy.weight = Mathf.Clamp01(volumeHappy.weight - Time.deltaTime);
                volumeAngry.weight = Mathf.Clamp01(volumeAngry.weight - Time.deltaTime);
                volumeFlee.weight = Mathf.Clamp01(volumeFlee.weight - Time.deltaTime);
                volumeBlock.weight = Mathf.Clamp01(volumeBlock.weight - Time.deltaTime);
                break;

            case NPCBehaviour.Routine:
                volumeHappy.weight = Mathf.Clamp01(volumeHappy.weight - Time.deltaTime);
                volumeAngry.weight = Mathf.Clamp01(volumeAngry.weight - Time.deltaTime);
                volumeFlee.weight = Mathf.Clamp01(volumeFlee.weight - Time.deltaTime);
                volumeBlock.weight = Mathf.Clamp01(volumeBlock.weight - Time.deltaTime);

                break;

            case NPCBehaviour.AFK:
                volumeHappy.weight = Mathf.Clamp01(volumeHappy.weight - Time.deltaTime);
                volumeAngry.weight = Mathf.Clamp01(volumeAngry.weight - Time.deltaTime);
                volumeFlee.weight = Mathf.Clamp01(volumeFlee.weight - Time.deltaTime);
                volumeBlock.weight = Mathf.Clamp01(volumeBlock.weight - Time.deltaTime);
                break;

            default:
                break;
        }
    }
}
