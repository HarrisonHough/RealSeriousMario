using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHeadCollisions : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDestructable destructable = collision.gameObject.GetComponent<IDestructable>();
        //if destructable and above
        if (destructable != null && collision.gameObject.transform.position.y > transform.position.y)
        {
            destructable.Destroy();
            _player.AddToScore(100);
        }
    }


}
