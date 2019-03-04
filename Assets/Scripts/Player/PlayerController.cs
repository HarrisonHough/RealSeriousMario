using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public delegate void JumpEvent();
    public JumpEvent onJumpEvent;
    public delegate void MovementStartEvent(float xInput);
    public MovementStartEvent onMovementStartEvent;
    public delegate void MovementStopEvent();
    public MovementStopEvent onMovementStopEvent;
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
        if (GameManager.Instance.currentState != GameState.InGame)
        {
            //to ensure movement stops
            onMovementInputUpdate?.Invoke(0);
            return;
        }

        float xInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime;

        onMovementInputUpdate?.Invoke(xInput);

        if (Input.GetKey(KeyCode.A))
        {
            onMovementStartEvent?.Invoke(-1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            onMovementStartEvent?.Invoke(1);
        }
        if (Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            onMovementStopEvent?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            onMovementStopEvent?.Invoke();
        }

        if (Input.GetButtonDown("Jump"))
        {
            onJumpEvent?.Invoke();
        }
    }


}
