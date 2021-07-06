using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shape : MonoBehaviour
{
    [Header("ShapeParameter")]
    [Range(3, 8)] public float side = 3;

    [ColorUsage(false, false), Space(12)]
    public Color color = Color.grey;
    private Color lastColor = Color.grey;

    [Space(4)]
    [Range(0, 1)] public float amountR = 0.5f;
    [Range(0, 1)] public float amountG = 0.5f;
    [Range(0, 1)] public float amountB = 0.5f;

    [Header("Components")]
    private SpriteRenderer sprRend = null;
    private Renderer rend;

    /// <summary>
    /// Renvoie l'écart entre deux couleur
    /// </summary>
    /// <param name="colorA">Couleur de référence</param>
    /// <param name="colorB">Couleur à soustraire</param>
    /// <returns></returns>
    public static Vector3 ColorDiff(Color colorA, Color colorB)
    {
        //Color have a convertion to Vec4 but not Vec3
        Vector4 diff = colorA - colorB;
        Vector3 colorDiff = diff;

        return colorDiff;
    }

    /// <summary>
    /// Calcul la distance entre deux couleur
    /// </summary>
    /// <param name="colorA">Couleur de référence</param>
    /// <param name="colorB">Couleur à soustraire</param>
    /// <returns></returns>
    public static float ColorDistance(Color colorA, Color colorB)
    {
        return Shape.ColorDiff(colorA, colorB).magnitude;
    }

    public void UpdateColor()
    {
        if (color != lastColor)
        {
            amountR = color.r;
            amountG = color.g;
            amountB = color.b;
        }
        else
        {
            color = new Color(amountR, amountG, amountB);
        }

        lastColor = color;
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        UpdateColor();

        if (sprRend == null)
        {
            sprRend = GetComponent<SpriteRenderer>();
        }
        else
        {
            sprRend.color = color;
        }

        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }
        else
        {
            if (rend.material.HasFloat("Sides"))
            {
                rend.material.SetFloat("Sides", side);
            }
            else
            {
                Debug.LogError("Shader stream data error", this);
            }
        }
    }

#endif
}