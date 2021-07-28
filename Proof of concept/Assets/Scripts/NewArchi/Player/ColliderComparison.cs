using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderComparison : MonoBehaviour
{

    List<GameObject> NPCAround = new List<GameObject>();
    float fleeScore;
    float happyScore;
    float attackScore;
    float blockScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(NPCAround.Count);
        ResetScores();
        CalculateScore();
        ApplyScore();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.GetComponent<PNJ>())
        {

            NPCAround.Add(collision.gameObject);
            Debug.Log("add" + collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PNJ>())
        {
            NPCAround.Remove(collision.gameObject);
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
        if (NPCAround.Count > 0)
        {
            foreach (GameObject pnj in NPCAround)
            {
                if (pnj.GetComponent<PNJ>().actualBehaviour == NPCBehaviour.Attack)
                {
                    attackScore = Mathf.Clamp(attackScore + 0.1f, 0, 1);
                }
                if (pnj.GetComponent<PNJ>().actualBehaviour == NPCBehaviour.Flee)
                {
                    fleeScore = Mathf.Clamp(fleeScore + 0.1f, 0, 1);
                }
                if (pnj.GetComponent<PNJ>().actualBehaviour == NPCBehaviour.Happy)
                {
                    happyScore = Mathf.Clamp(happyScore + 0.1f, 0, 1);
                }
                if (pnj.GetComponent<PNJ>().actualBehaviour == NPCBehaviour.Block)
                {
                    blockScore = Mathf.Clamp(blockScore + 0.1f, 0, 1);
                }
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
