using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent TriggerEvents;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TriggerEvents.Invoke();
    }
}
