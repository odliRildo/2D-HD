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

    public GameObject stepRayLower;
    public GameObject stepRayUpper;
    public float stepHeight ;
    public float stepSmooth ;

    public float distToGround = 1f;
    public float togroundspeed;
    private void Awake(){
        playerControls = new PlayerInputActions();
        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x,stepHeight,stepRayUpper.transform.position.z);

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

        stepClimb();
        if(!isGrounded()){
            rb.velocity += new Vector3(0,-togroundspeed,0);
            Debug.Log("Not Grounded");
        }
    }

    private void Fire(InputAction.CallbackContext context){
        animator.SetTrigger("Attack");
        
    }

    void stepClimb()
    {
         RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.2f))
            {
                rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }

         RaycastHit hitLower45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f,0,1), out hitLower45, 0.1f))
        {

            RaycastHit hitUpper45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f,0,1), out hitUpper45, 0.2f))
            {
                rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }

        RaycastHit hitLowerMinus45;
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f,0,1), out hitLowerMinus45, 0.1f))
        {

            RaycastHit hitUpperMinus45;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f,0,1), out hitUpperMinus45, 0.2f))
            {
                rb.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
            }
        }
    }

    bool isGrounded(){
        if(Physics.Raycast(transform.position,Vector3.down, distToGround + 0.1f)){
            return false;
        }
        return true;
    }



}
