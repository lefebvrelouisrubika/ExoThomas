using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PNJGroup : MonoBehaviour
{
    [Header("Group Shape aimed")]
    public bool groupOverwrite = false;
    [Space(10)]
    [Range(3, 8)] public float targetSide = 3;
    [ColorUsage(false, false)] public Color targetColor = Color.HSVToRGB(0,0.36f,0.75f);
    [HideInInspector] public float targetHue = 0;
    [Space(10)]
    [Range(0f, 1f)] public float similarthreshold;
    [Range(0f, 1f)] public float neutralthreshold;

    [Header("Post Process Behavior")]
    public NPCBehaviour stateSimilar = NPCBehaviour.Happy;
    public NPCBehaviour stateNeutral = NPCBehaviour.Neutral;
    public NPCBehaviour stateFaraway = NPCBehaviour.Attack;
    private NPCBehaviour actualBehaviour = NPCBehaviour.Happy;

    [Header("Player Value")]
    public float playerLookSimilarity = 0f;
    public float playerDetectionThreshold = 2f;
    private float playerDistance;
    public PlayerController player;

    [Header("Group PNJ ")]
    public List<PNJ> allPNJ = new List<PNJ>();

    void Start()
    {
        player = PlayerController.instance;
        for (int i = 0; i < allPNJ.Count; i++)
        {
            allPNJ[i].group = this;
            allPNJ[i].player = player;
        }
    }

    void Update()
    {
        DetectingPlayer();

        if (groupOverwrite)
        {
            EvaluatePlayer();
            CallPlayerProximity();
        }

        ChooseBehavior();
    }

    public void DetectingPlayer()
    {
        //Calcul Distance
        playerDistance = (player.transform.position - transform.position).magnitude;

        if (playerDistance < playerDetectionThreshold)
        {
            CallPlayerInArea(true);
        }
        else
        {
            CallPlayerInArea(false);
        }
    }

    private void EvaluatePlayer()
    {
        //Get group target Hue
        Color.RGBToHSV(targetColor, out targetHue, out _, out _);

        float sideProxi = Shape.SideDistance(targetSide, player.side);
        //compare distance (1- / oneMinus to convert distance in proximity)
        float colorProxi = 1-Shape.HueDistance(targetHue, player.hue);

        playerLookSimilarity = (sideProxi + colorProxi) / 2;
    }

    private void ChooseBehavior()
    {
        if (playerLookSimilarity < neutralthreshold)
        {
            actualBehaviour = stateFaraway;
        }
        else
        if (playerLookSimilarity < similarthreshold)
        {
            actualBehaviour = stateNeutral;
        }
        else
        {
            actualBehaviour = stateSimilar;
        }
    }
    private void CallPlayerInArea(bool playerIsInArea)
    {
        for (int i = 0; i < allPNJ.Count; i++)
        {
            allPNJ[i].playerInArea = playerIsInArea;
        }
    }
    private void CallPlayerProximity()
    {
        for (int i = 0; i < allPNJ.Count; i++)
        {
            allPNJ[i].playerLookProximity = playerLookSimilarity;
        }
    }

    #region Debug
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.color = targetColor;
        Handles.DrawWireDisc(transform.position, Vector3.forward, playerDetectionThreshold);
        Handles.color = Color.white;
    }

#endif
    #endregion
}
