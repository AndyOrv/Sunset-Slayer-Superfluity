using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
-- Author: Andrew Orvis
-- Description: A player controller class allowing capabilies to move a player in third person jump and sprint
 */


public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;

    [Header("Movement")]
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float runSpeed = 12.0f;
    [SerializeField] float gravity = -13.0f;

    [SerializeField] private float runBuildUpSpeed;
    [SerializeField] private KeyCode runKey;

    [Header("Mouse")]
    //[SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    //[SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool lockCursor = true;

    private float movementSpeed;

    //float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    public CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;
    
    [Header("Jump")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;


    [Header("Slope")]
    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    private bool isJumping;

    [Header("Game Options")]
    public bool Paused = false;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        sliding,
        air,
        idle
    }


    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        cursorLock();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        StateHandler();

        if (!Paused)
        {
            UpdateMovement();
        }
    }

    #region movement
    // apply forces in correct direction for player movement
    void UpdateMovement()
    {
       
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (orientation.forward * currentDir.y + orientation.right * currentDir.x) * movementSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if ((currentDir.x != 0 || currentDir.y != 0) && OnSlope())
            controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);

        SetMovementSpeed();
        JumpInput();

    }

    private void SetMovementSpeed()
    {
        if (Input.GetKey(runKey))
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
        else
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
    }
    #endregion

    #region jump code
    private void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    //applies collision flags for plyaer to hit off roofs and works through animation curve
    private IEnumerator JumpEvent()
    {
        controller.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);

        controller.slopeLimit = 45.0f;
        isJumping = false;
    }

    //Applies additonal force to player to allow for smooth decent on slopes as gravity alone wont over come movement speed and causes player to jitter
    private bool OnSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up)
                return true;
        return false;
    }
    #endregion

    private void StateHandler()
    {
        //Mode - Idle
        if (grounded && controller.velocity.magnitude <= 0.1)
        {
            state = MovementState.idle;
        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(runKey))
        {
            state = MovementState.sprinting;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
        }

        // Mode - Air
        else if(!grounded)
        {
            state = MovementState.air;
        }
    }

    void cursorLock()
    {
        if (!Paused)
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}