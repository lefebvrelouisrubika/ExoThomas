using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ButtonCollider : MonoBehaviour
{
    public UnityEvent onInteraction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (onInteraction != null)
            {
                onInteraction.Invoke();
            }
        }
    }

}
