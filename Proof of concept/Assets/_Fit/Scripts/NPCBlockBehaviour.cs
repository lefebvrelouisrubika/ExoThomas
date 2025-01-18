using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBlockBehaviour : MonoBehaviour
{
    public GameObject[] NPCs;
    public GameObject[] blockingPositions;
    private Vector3[] normalPositions;
    private Vector3[] blockPositions;
    private bool[] gatekeeping;
    public float moveToBlockSpeed;

    public void Start()
    {
        normalPositions = new Vector3[NPCs.Length];
        blockPositions = new Vector3[blockingPositions.Length];
        gatekeeping = new bool[NPCs.Length];

        for (int a = 0; a < NPCs.Length; a++)
        {
            normalPositions[a] = NPCs[a].transform.position;
        }

        for (int b = 0; b < blockingPositions.Length; b++)
        {
            blockPositions[b] = blockingPositions[b].transform.position;
        }
    }

    private void Update()
    {
        NPCStateCheck();

        if (AllChecked(gatekeeping))
        {
            Block();
            //Debug.Log("Block");
        }
        else if (!AllChecked(gatekeeping))
        {
            MoveToNormalPos();
            //Debug.Log("Move");
        }
    }

    private void NPCStateCheck()
    {
        for (int c = 0; c < NPCs.Length; c++)
        {
            gatekeeping[c] = NPCs[c].GetComponent<NPC>().wantToBlock;
        }
    }

    private bool AllChecked(bool[] gates)
    {
        for (int i = 0; i < gates.Length; i++)
        {
            if (!gates[i])
            {
                return false;
            }
        }
        return true;
    }

    private void Block()
    {
        for (int d = 0; d < NPCs.Length; d++)
        {
            NPCs[d].transform.position = Vector3.MoveTowards(NPCs[d].transform.position, blockPositions[d], moveToBlockSpeed * Time.deltaTime);
        }
    }

    private void MoveToNormalPos()
    {
        for (int e = 0; e < NPCs.Length; e++)
        {
            NPCs[e].transform.position = Vector3.MoveTowards(NPCs[e].transform.position, normalPositions[e], moveToBlockSpeed * Time.deltaTime);
        }

        //Debug.Log("Returned");
    }
}
