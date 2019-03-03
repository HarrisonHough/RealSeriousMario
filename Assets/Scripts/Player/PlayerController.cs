using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public delegate void JumpEvent();
    public JumpEvent onJumpEvent;
    public delegate void MovementInputUpdate(float xInput);
    public MovementInputUpdate onMovementInputUpdate;

    private bool _isGrounded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.InGame)
        {
            //to ensure movement stops
            onMovementInputUpdate?.Invoke(0);
            return;
        }

        float xInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime;

        onMovementInputUpdate?.Invoke(xInput);
        if (Input.GetButtonDown("Jump"))
        {
            onJumpEvent?.Invoke();
        }
    }


}
