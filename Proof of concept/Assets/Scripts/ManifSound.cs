using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManifSound : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playLaunched = false;
    public AudioClip sonManif;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && playLaunched == false)
        {
            Soundmanager.Instance.PlayMusic(sonManif, 0f);
            playLaunched = true;
        }
            Soundmanager.Instance.ChangeVolume(0.6f);


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Soundmanager.Instance.ChangeVolume(0);
        }
    }
}
