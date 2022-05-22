using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public PlayerInputActions playerControls;
    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction fire;

    public Animator animator;


    private void Awake(){
        playerControls = new PlayerInputActions();
    }


    private void OnEnable(){
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    private void OnDisable(){
        move.Disable();
        fire.Disable();
    }
    


    void Update(){
        

        moveDirection = move.ReadValue<Vector2>();

        animator.SetFloat("Horizontal",moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Speed",moveDirection.sqrMagnitude);
    }

    void FixedUpdate(){
        rb.velocity = new Vector3( moveDirection.x*moveSpeed , 0, moveDirection.y*moveSpeed );
    }

    private void Fire(InputAction.CallbackContext context){
        animator.SetTrigger("Attack");
        
    }
}
