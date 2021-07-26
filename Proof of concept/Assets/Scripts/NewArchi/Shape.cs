using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shape : MonoBehaviour
{
    [Header("ShapeParameter")]
    protected const float MinSide = 3;
    protected const float MaxSide = 8;
    [Range(3, 8)] public float side = 3;

    [ColorUsage(false, false), Space(12)]
    public Color color = Color.grey;
    protected Color lastColor = Color.grey;

    [Space(4)]
    [Range(0, 1)] public float hue = 0.5f;
    [Range(0, 1)] public float satur = 0.5f;
    [Range(0, 1)] public float value = 0.5f;

    public float hueVisual = 0.5f;

    [Header("Components")]
    protected SpriteRenderer sprRend = null;
    protected Renderer rend;
    protected Material mat;

    [Range(-0.1f, 0.1f)]
    public float testOffsetHue = 0f;

    public virtual void Awake()
    {
        sprRend = GetComponent<SpriteRenderer>();
        
        rend = GetComponent<Renderer>();
        mat = rend.sharedMaterial;
    }

    /// <summary>
    /// Calcul la distance entre deux couleur
    /// </summary>
    /// <returns></returns>
    public static float HueDistance(float hueOriginal, float hueCompared)
    {
        float hueDist = Mathf.Abs(hueOriginal - hueCompared);

        if (hueDist > 0.5f)
        {
            if (hueOriginal > 0.5f)
            {
                hueCompared++;
                hueDist = Mathf.Abs(hueOriginal - hueCompared);
            }
            else
            {
                hueCompared--;
                hueDist = Mathf.Abs(hueOriginal - hueCompared);
            }
        }

        hueDist = 2 * hueDist;
        return hueDist;
    }

    /// <summary>
    /// Calcul la distance entre deux shape
    /// </summary>
    /// <returns></returns>
    public static float SideDistance(float SideOriginal, float SideCompared)
    {
        float result = Mathf.Abs((SideOriginal - MinSide) - (SideCompared - MinSide)) / (MaxSide-MinSide);
        return 1-result;
    }

    public virtual void UpdateColor()
    {

        if (color != lastColor)
        {
            //HSV
            Color.RGBToHSV(color, out hue, out satur, out value);

        }
        else
        {
            //HSV
            hueVisual = Mathf.Ceil((hue - 0.022f) * 8) / 8;
            hueVisual += -0.042f - 0.0115f;
            color = Color.HSVToRGB(hueVisual, satur, value);
        }

        lastColor = color;
        sprRend.color = color;
    }

    public void UpdateSide()
    {
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }

        mat.SetFloat("Sides", side);
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (sprRend == null)
        {
            sprRend = GetComponent<SpriteRenderer>();
        }
        else
        {
            UpdateColor();
        }

        if (rend == null)
        {
            rend = GetComponent<Renderer>();
            mat = rend.sharedMaterial;
        }
        else
        {
            UpdateSide();
        }
    }

#endif
}