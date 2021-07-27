using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderComparison : MonoBehaviour
{

    List<GameObject> NPCAround;
    int fleeScore;
    int happyScore;
    int attackScore;
    int blockScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ResetScores();
        CalculateScore();
        ApplyScore();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.GetComponent<PNJ>())
        {
            NPCAround.Add(collision.gameObject);
        }
    }

    public void ResetScores()
    {
        fleeScore = 0;
        happyScore = 0;
        attackScore = 0;
        blockScore = 0;
    }

    public void CalculateScore()
    {
        foreach(GameObject pnj in NPCAround)
        {
            if (pnj.GetComponent<PNJ>().actualBehaviour == NPCBehaviour.Attack)
            {
                attackScore = Mathf.Clamp(attackScore + 1,0,10);
            }
            if (pnj.GetComponent<PNJ>().actualBehaviour == NPCBehaviour.Flee)
            {
                fleeScore = Mathf.Clamp(fleeScore + 1, 0, 10);
            }
            if (pnj.GetComponent<PNJ>().actualBehaviour == NPCBehaviour.Happy)
            {
                happyScore = Mathf.Clamp(happyScore + 1, 0, 10);
            }
            if (pnj.GetComponent<PNJ>().actualBehaviour == NPCBehaviour.Block)
            {
                blockScore = Mathf.Clamp(blockScore + 1, 0, 10);
            }
        }
    }

    public void ApplyScore()
    {
        PostProcessManager.Instance.attackScore = attackScore;
        PostProcessManager.Instance.fleeScore = fleeScore;
        PostProcessManager.Instance.happyScore = happyScore;
        PostProcessManager.Instance.blockScore = blockScore;
    }
}
