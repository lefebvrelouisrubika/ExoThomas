using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Arrivee;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject Part2;
    private bool canMove = false;
    private float distance;
    public float lerpSpeed;
    private bool coroutinable = true;
    public AudioClip chatSound1;
    public AudioClip chatSound2;
    private bool sound1Done;
    public float noiseDelay;
    public float noiseDelayReductionFactor;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        distance = Vector3.Distance(Arrivee.position, this.transform.position);

        if (canMove == true)
        {
            if (distance > 0.1f)
            {
                this.transform.position = Vector3.Lerp (this.transform.position, Arrivee.position, lerpSpeed *Time.deltaTime);
            }
            if (distance <= 0.1f)
            {
                canMove = false;
                Door1.SetActive(false);
                Door2.SetActive(false);
                Part2.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (coroutinable == true)
            {
                StartCoroutine("WaitToMove");
                StartCoroutine("PingProgressive");
            }
        }
    }
    
    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(0.5f);
        canMove = true;
        coroutinable = false;
        Door1.SetActive(true);
        Door2.SetActive(true);
    }

    IEnumerator PingProgressive()
    {
        float firstDistance = distance;

        while (distance > 0.1f)
        {
            Debug.Log("NewTwitterSound");
            if (sound1Done == false)
            {
                Soundmanager.Instance.PlaySFX(chatSound1, 1);
                sound1Done = true;
            }
            else if (sound1Done == true)
            {
                Soundmanager.Instance.PlaySFX(chatSound2, 1);
                sound1Done = false;
            }

            yield return new WaitForSeconds(noiseDelay);
            noiseDelay = noiseDelay * noiseDelayReductionFactor;
        }
    }
}
