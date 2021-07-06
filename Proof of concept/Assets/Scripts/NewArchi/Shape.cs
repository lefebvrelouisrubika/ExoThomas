using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Shape : MonoBehaviour
{
    [Header("ShapeParameter")]
    [Range(3, 8)] public float side = 3;
    [ColorUsage(false, false)] public Color color = Color.red;

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

}
