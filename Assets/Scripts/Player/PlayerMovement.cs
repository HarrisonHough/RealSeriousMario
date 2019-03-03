using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    private Vector3 _targetVelocity;
    private Rigidbody2D _rigidbody2D;
    private float _xInput = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _targetVelocity = Vector3.zero;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.onMovementInputUpdate += UpdateVelocity;
        }
    }


    public void UpdateVelocity(float xInput)
    {
        _xInput = xInput;
    }

    public void MatchVelocity()
    {

    }


    private void FixedUpdate()
    {
        //_rigidbody2D.transform.Translate(_velocity * _moveSpeed );
        _targetVelocity = Vector2.zero;
        _targetVelocity = new Vector2(_xInput * _moveSpeed * 200, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = _targetVelocity;
    }
}