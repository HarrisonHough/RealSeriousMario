using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [SerializeField]
    private float _jumpPower = 10f;
    public LayerMask platformLayer;

    private Rigidbody2D _rigidbody2D;
    private CircleCollider2D _CircleCollider2D;

    public bool isGrounded { get; private set; }
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
        if (!CheckIsGrounded())
            return;

        _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }

    public bool CheckIsGrounded()
    {
        bool isGrounded = false;
        Vector3 playerBottomLeft = new Vector3(transform.position.x - (_CircleCollider2D.radius), transform.position.y - (_CircleCollider2D.radius), 0);
        for (int i = 0; i < 3; i++)
        {
            if (Physics2D.Raycast(transform.position, transform.TransformDirection(-Vector3.up), (_CircleCollider2D.radius) + 0.1f, platformLayer))
                isGrounded = true;
        }
        return isGrounded;
    }
}