using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10f;
    [SerializeField]
    private float _movementSmoothing = 0.5f;
    private Vector3 _velocity;
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _velocity = Vector3.zero;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.onMovementInputUpdate += UpdateVelocity;
        }
    }


    public void UpdateVelocity(float xInput)
    {
        _velocity.x = xInput;
    }

    public void 


    private void FixedUpdate()
    {
        //_rigidbody2D.transform.Translate(_velocity * _moveSpeed );

        Vector3 targetVelocity = new Vector2(_velocity.x * _moveSpeed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = targetVelocity;
    }
}
