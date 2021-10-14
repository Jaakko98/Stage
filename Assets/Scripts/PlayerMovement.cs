using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float airSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float distanceToGround;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, distanceToGround, groundMask);

        if(isGrounded == true && velocity.y < 0)    //stops applying gravity when grounded
        {
            velocity.y = -2f;
        }

        float moveZdir = Input.GetAxis("Vertical");
        float moveXdir = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveXdir, 0, moveZdir);
        moveDirection = transform.TransformDirection(moveDirection);    //tries to move character in facing direction

        if (isGrounded)
        {
            moveDirection *= runSpeed;
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }
        else if (!isGrounded)
        {
            moveDirection *= airSpeed;
        }
        
        

        

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;     //calculating gravity
        controller.Move(velocity * Time.deltaTime);     //deltaTime twice cause math
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
}
