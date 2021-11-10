using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //VARIABLES

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float airSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float distanceToGround;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    [SerializeField] private float mouseSensitivity; //for Rotate()


    //REFERENCES

    private CharacterController controller;
    public GameObject gm;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        gm.GetComponent<GameManager>().checkpointsReached = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up, mouseX);
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= runSpeed;
        }
        else
        {
            moveDirection *= walkSpeed;
        }
        if (isGrounded)
        {
            
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }
        /*
        else if (!isGrounded)
        {
            moveDirection *= airSpeed;
        }
        */
        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;     //calculating gravity
        controller.Move(velocity * Time.deltaTime);     //deltaTime twice because math
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint"))
        {
            Debug.Log("hit checkpoint");
            gm.GetComponent<GameManager>().CheckpointReached();
            other.tag = "Used";
        }
    }
}
