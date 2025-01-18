using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManifSound : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip sonManif;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() )
        {
            Soundmanager.Instance.PlayMusic(sonManif, 0.1f);
            StartCoroutine(StartSound());
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Soundmanager.Instance.ChangeVolume1(0);
            StartCoroutine(EndSound());

        }
    }

    IEnumerator EndSound()
    {

        yield return new WaitForSeconds (1);
        Soundmanager.Instance.StopMusic();
    }

    IEnumerator StartSound()
    {

        yield return new WaitForSeconds (1);
        Soundmanager.Instance.ChangeVolume1(0.4f);
    }
}
