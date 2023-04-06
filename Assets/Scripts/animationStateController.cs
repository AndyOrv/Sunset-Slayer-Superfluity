using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    public PlayerController plyr;
    Animator anim;
    private float playerSpeed;
    private float playerStrafeSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        StateHandler();
        //StateVelocityHandler();
        Debug.Log(playerStrafeSpeed);
    }

    private void FixedUpdate()
    {
        playerSpeed = Mathf.Abs(plyr.controller.velocity.x);
        playerStrafeSpeed = plyr.controller.velocity.z;
        StateVelocityHandler();
    }

    private void StateHandler()
    {
        //bool isWalkingHash = anim.GetBool("isWalking");
        //bool isRunningHash = anim.GetBool("isRunning");
        //bool isFloatingHash = anim.GetBool("isFloating");

        if (plyr.state == PlayerController.MovementState.air)
        {
            anim.SetBool("isFloating", true);
            //Debug.Log("is Floating");
        }

        #region
        /*
        else if (plyr.state == PlayerController.MovementState.sprinting)
        {
            anim.SetBool("isRunning", true);
            //Debug.Log("is Running");
        }
        else if (plyr.state == PlayerController.MovementState.walking)
        {
            anim.SetBool("isWalking", true);
            //Debug.Log("is Walking");
        }
        */
        #endregion

        else
        {
            //Debug.Log("Idle");
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isFloating", false);
        }
    }

    private void StateVelocityHandler()
    {
        anim.SetFloat("Velocity Z", playerSpeed);
        anim.SetFloat("Velocity X", playerStrafeSpeed);
    }
}
