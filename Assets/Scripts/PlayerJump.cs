using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [SerializeField]
    private float _jumpPower = 10f;

    private Rigidbody2D _rigidbody2D;
    private CircleCollider2D _CircleCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _CircleCollider2D = GetComponent<CircleCollider2D>();
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.onJumpEvent += Jump;
        }
    }

    public void Jump()
    {
        if (_rigidbody2D.velocity.y != 0)
            return;
        //_rigidbody2D.AddForce(Vector2.up * _jumpPower);
        //_rigidbody2D.velocity += Vector2.up * _jumpPower;
        _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

    }
}
