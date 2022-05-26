using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public PlayerInputActions playerControls;
    Vector2 moveDirection = Vector2.zero;
    public Animator animator;

    private InputAction move;
    private InputAction fire;

    [Header("Falling")]
    public LayerMask Groundlayer;
    public float SphereOffset;
    private Vector3 SphereCastPosition;
    private Vector3 SphereForwardPosition;
    public float downForce;
    public GameObject stepRayUpper;
    public GameObject stepRayLower;
    public float stepSmooth;

    [Header("Movement Flags")]
    public bool isGrounded;
    public bool goUp;

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

        groundCheck();
    }

    private void Fire(InputAction.CallbackContext context){
        animator.SetTrigger("Attack");
        
    }


    void groundCheck(){
        RaycastHit hit;
        SphereCastPosition = new Vector3(transform.position.x,transform.position.y+SphereOffset, transform.position.z);
        if(Physics.SphereCast(SphereCastPosition,0.2f, -1*Vector3.up, out hit, Groundlayer)){
            isGrounded = true;
            
        }
        else{
            isGrounded = false;
            rb.AddForce(0,-1*downForce,0);
        }
        stepClimb();
    }

    void stepClimb(){
        RaycastHit hitlower;

        if(Physics.Raycast(stepRayLower.transform.position,Vector3.forward,out hitlower, 0.1f)){
            RaycastHit hitUpper;
            if(!Physics.Raycast(stepRayUpper.transform.position, Vector3.forward, out hitUpper,0.2f)){
                goUp = true;
                rb.position -= new Vector3(0f,-stepSmooth,0f);
            }
        }
    }



    void OnDrawGizmosSelected(){
        Vector3 sph = SphereCastPosition;
        sph.y-=1f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(sph,0.2f);
    }


}
