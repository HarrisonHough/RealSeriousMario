using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerJump))]
public class PlayerHeadCollisions : MonoBehaviour
{
    public LayerMask platformLayer;
    private PlayerJump _playerJump;
    
    private CircleCollider2D _circleCollider2D;

    private void Start()
    {
        _playerJump = GetComponent<PlayerJump>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!_playerJump.isGrounded)
        {

        }
    }

    private void CheckHeadCollisions()
    {
       
        RaycastHit2D results = new RaycastHit2D();
       
    }
}
