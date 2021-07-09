using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PNJGroup : MonoBehaviour
{
    [Header("Group Shape aimed")]
    [Range(3, 8)] public float targetSide = 3;
    [ColorUsage(false, false)] public Color targetColor = Color.HSVToRGB(0,0.36f,0.75f);
    [HideInInspector] public float targetHue = 0;
    [Space(10)]
    [Range(0f, 1f)] public float similarthreshold;
    [Range(0f, 1f)] public float neutralthreshold;

    [Header("Player Value")]
    public float playerSimilarity = 0f;
    public float playerDetectionThreshold = 2f;
    private PlayerController player;

    void Start()
    {
        player = PlayerController.instance;

    }

    void Update()
    {
        
    }

    public void DetectingPlayer()
    {
        if((player.transform.position - transform.position).magnitude < playerDetectionThreshold)
        {
            //Player Detected
            EvaluatePlayer();

        }
        else
        {
            //Return to classique state
        }
    }

    private void EvaluatePlayer()
    {
        float sideProxi = 1 - Shape.SideDistance(targetSide, player.side);
        Color.RGBToHSV(targetColor, out targetHue, out _, out _);
        float colorProxi = 1 - Shape.HueDistance(targetHue, player.hue);

        playerSimilarity = (sideProxi + colorProxi) / 2;
    }

    #region Debug
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.color = targetColor;
        Handles.DrawWireDisc(transform.position, Vector3.up, playerDetectionThreshold);
        Handles.color = Color.white;
    }

#endif
    #endregion
}
