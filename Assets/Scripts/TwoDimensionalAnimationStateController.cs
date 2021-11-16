using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{
    //VARIABLES
    private float velocityZ = 0.0f;
    private float velocityX = 0.0f;
    public float maxRunVelocity = 2.0f;
    public float maxWalkVelocity = 0.5f;
    public float acceleration = 2.0f;

    //REFERENCES
    Animator animator;

    //METHODS

    // Start is called before the first frame update
    void Start()
    {
        //search the gameObject this script is attached to and get Animator component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        //set max velocity. Used only for forward movement. I want to reward player for using forward motion.
        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        //walk forward or stop moving back and run forward ir runPressed
        if(forwardPressed && velocityZ < currentMaxVelocity || !backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        //walk left or stop moving right
        if (leftPressed && velocityX > -maxWalkVelocity || !rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        //walk right or stop moving left
        if (rightPressed && velocityX < maxWalkVelocity || !leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        //walk back or stop moving forward
        if (backPressed && velocityZ > -maxWalkVelocity || !forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        //reset velocityY
        if(!forwardPressed && !backPressed && velocityZ != 0.0f && (velocityZ < 0.05f && velocityZ > -0.05f))
        {
            velocityZ = 0.0f;
        }
        //reset velocityX
        if(!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX < 0.05f && velocityX > -0.05f))
        {
            velocityX = 0.0f;
        }
        //next if statements handle close to max value situations and make them exact to them

        //lock forward max speed
        if(forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        //decelerate to walk from running forward
        else if(forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
            //round to currentMaxVelocity when coming from runnig to walking
            if(velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        //round to currentMaxVelocity when coming from idle to walking
        else if(forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }
        //left, right and back are different from forward because there is no running in those directions
        //round left walk
        if(leftPressed && velocityX < -maxWalkVelocity)
        {
            velocityX = -maxWalkVelocity;
        }
        //round right walk
        if(rightPressed && velocityX > maxWalkVelocity)
        {
            velocityX = maxWalkVelocity;
        }
        //round back walk
        if(backPressed && velocityZ < -maxWalkVelocity)
        {
            velocityZ = -maxWalkVelocity;
        }

        animator.SetFloat("VelocityZ", velocityZ);
        animator.SetFloat("VelocityX", velocityX);
    }
}
