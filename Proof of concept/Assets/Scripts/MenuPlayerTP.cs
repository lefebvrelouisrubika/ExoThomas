using UnityEngine;
using UnityEditor;

public class MenuPlayerTP : MonoBehaviour
{
    private PlayerController player;

    [Header("Parameter")]
    [SerializeField] Vector2 menuPos = Vector2.one;
    [SerializeField] Vector2 creditPos = Vector2.one;

    void Start()
    {
        player = PlayerController.instance;
    }

    public void TP2Menu()
    {
        player.transform.position = new Vector3(menuPos.x, menuPos.y, 0);
    }
    public void TP2Credit()
    {
        player.transform.position = new Vector3(creditPos.x, creditPos.y, 0);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Handles.color = Color.white;
        Handles.color = Color.magenta;
        Handles.DrawWireDisc(new Vector3(menuPos.x, menuPos.y, 0), Vector3.forward, 1f);
        Handles.DrawWireDisc(new Vector3(creditPos.x, creditPos.y, 0), Vector3.forward, 1f);
        Handles.color = Color.white;
    }

#endif
}
