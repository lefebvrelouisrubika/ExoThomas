using UnityEngine;
using UnityEngine.UI;

public class PlayerChromaticWheel : MonoBehaviour
{
    [Header("Components")]
    public PlayerController player;
    [Space(5)]
    public Image chromaWheel = null;
    [Space(5)]
    private Material mat;


    void Awake()
    {
        mat = chromaWheel.materialForRendering;
    }

    void Update()
    {
        mat.SetFloat("baseHue", player.baseHue);

        float hueSend = player.hue;
        hueSend = hueSend < 0 ? hueSend + 1 : hueSend;
        hueSend %= 1;

        mat.SetFloat("hue", hueSend);
    }
}
