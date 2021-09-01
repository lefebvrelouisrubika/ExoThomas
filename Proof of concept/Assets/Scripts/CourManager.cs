using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourManager : MonoBehaviour
{
    public GameObject[] NPCGroups;
    private bool[] noSee;
    [SerializeField] private bool empty;
    [SerializeField] private int count;
    public GameObject gateOut;
    
    public float timer;
    public float timerClose;
    private bool timerLaunched;
    private bool playLaunched;
    public AudioClip courAmbiance;

    // Start is called before the first frame update
    void Start()
    {
        timerLaunched = false;
        playLaunched = false;
        gateOut.SetActive(true);
        
        count = NPCGroups.Length;
        empty = false;
        noSee = new bool[NPCGroups.Length];

        for (int i = 0; i < NPCGroups.Length; i++)
        {
            noSee[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (empty == false)
        {
            CheckGroup();
            UpdateAmbiance();
        }
        else if (empty == true)
        {
            gateOut.SetActive(false);
        }
    }

    private void CheckGroup()
    {
        //Debug.Log("Check");
        for (int a = 0; a < NPCGroups.Length; a++)
        {
            if (NPCGroups[a].GetComponent<PNJGroup>().allGone == true && noSee[a] == false)
            {
                Debug.Log("go");
                noSee[a] = true;
                count -= 1;
            }
        }

        if (count == 0)
        {
            StartCoroutine(TimerForGate());
            
            //empty = true;
        }
    }

    private void UpdateAmbiance()
    {
        switch (count)
        {
            case 0:
                Soundmanager.Instance.ChangeVolume(0);
                break;

            case 1:
                Soundmanager.Instance.ChangeVolume(0.33f);
                break;

            case 2:
                Soundmanager.Instance.ChangeVolume(0.66f);
                break;

            case 3:
                Soundmanager.Instance.ChangeVolume(0.8f);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && playLaunched == false)
        {
            Soundmanager.Instance.PlayMusic(courAmbiance, 1);
            playLaunched = true;
            
        }

        //if (timerLaunched == false)
        //{
        //    timerLaunched = true;

        //    StartCoroutine(TimerForGate());
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Soundmanager.Instance.PlayMusic(courAmbiance, 0);
            
        }
    }

    private IEnumerator TimerForGate()
    {
        yield return new WaitForSeconds(timer);
        empty = true;

    }

}
