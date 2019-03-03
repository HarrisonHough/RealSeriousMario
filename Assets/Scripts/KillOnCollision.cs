using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KillOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var killableObject = collision.gameObject.GetComponent<IKillable>();

        if (killableObject != null)
        {
            killableObject.Kill();
        }
    }
}
