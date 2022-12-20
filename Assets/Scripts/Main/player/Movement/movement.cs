using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movement : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Movement")]
    public Wallrun Wallrun;
    public Climb climb;
    public State state;
    [SerializeField]
    private float baseSpeed, crouchSpeed, walkSpeed, sprintSpeed;
    public Vector3 moveDir, groundNormal = Vector3.up;
    private float horizontalInput, verticalInput;
    public Rigidbody rb;
    public CapsuleCollider col;
    public Transform orientation;

    [Header("Jump")]
    public float jumpF;
    public float baseJumpF;
    public float sprintJumpF;
    public float jumpCd;
    public float airmult;
    bool ready;

    [Header("Slide")]
    private float slideTime = 2f;
    public float slideTimer;
    public float slideCd;
    private float slideSpeed;
    private float slideMult = 200f;
    private bool sliding;

    [Header("Crouch")]
    private float startHeight;
    public float crouchHeight;
    private bool crouching;

    [Header("Drag")]
    public LayerMask floor;
    public bool ground;
    public float baseDrag, height;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;

    public enum State
    {
        Walk,
        Sprint,
        Wallrunning,
        Climbing,
        Crouching,
        Sliding,
        Air
    }

    private void StateHandler()
    {
        if (Input.GetKey(crouchKey) && !Input.GetKey(sprintKey) && ground)
        {
            state = State.Crouching;
            baseSpeed = Mathf.Lerp(baseSpeed,crouchSpeed,.1f);
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, crouchHeight, 0.1f), transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, startHeight, 0.1f), transform.localScale.z);
        }
        if(Input.GetKey(crouchKey) && Input.GetKey(sprintKey) && slideTimer>=0 && ground)
        {
            state = State.Sliding;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, crouchHeight, 0.1f), transform.localScale.z);
            Slide();
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, startHeight, 0.1f), transform.localScale.z);
            UnSlide();
        }
        if (ground && !crouching && !sliding && Input.GetKey(sprintKey) && Input.GetAxis("Vertical") > 0.9 && Input.GetAxis("Horizontal") == 0)
        {
            state = State.Sprint;
            jumpF = sprintJumpF;
            baseSpeed = Mathf.Lerp(baseSpeed, sprintSpeed, .005f);
        }
        else if (ground && !crouching && !sliding)
        {
            jumpF = baseJumpF;
            state = State.Walk;
            baseSpeed = Mathf.Lerp(baseSpeed, walkSpeed, .01f);
        }
        else if (Wallrun.wallrunning && !ground)
        {
            state = State.Wallrunning;
        }
        else if(climb.climbing && !ground)
        {
            state = State.Climbing;
        }
        else if(!ground)
        {
            state = State.Air;
        }
    }

    private void Inputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && ready && ground)
        {
            ready = false;
            jump();
            Invoke(nameof(resetjump), jumpCd);
        }

        if (Input.GetKeyDown(crouchKey))
        {
            crouching = true;
            
        }
        if (Input.GetKeyUp(crouchKey))
        {
            crouching = false;
        }
    }
    private void Start()
    {
        slideCd = 0;
        slideTimer = slideTime;
        gameManager.fovSlider.value = PlayerPrefs.GetFloat("Fov");
        gameManager.sensSlider.value = PlayerPrefs.GetFloat("Sens");
        gameManager.volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        ready = true;
        rb = GetComponent<Rigidbody>();
        startHeight = transform.localScale.y;
        rb.freezeRotation = true;
        Cursor.visible = false;
    }

    private void movePlayer()
    {
        if (climb.exitingWall || sliding) return;
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (ground)
        {
            rb.AddForce(moveDir.normalized * baseSpeed * 10f, ForceMode.Force);
        }
        else if (!ground)
        {
            rb.AddForce(moveDir.normalized * baseSpeed * 10f * airmult, ForceMode.Acceleration);
        }
    }

    private void Update()
    {
        StateHandler();
        Inputs();
        speedControl();
        RaycastHit hit;
        height = transform.localScale.y;
        ground = Physics.SphereCast(transform.position, 0.3f, Vector3.down,out hit, height, floor);
        if (ground)
        {
            rb.drag = baseDrag;
        }
        else
        {
            rb.drag = 0;
        }
        
    }

    private void FixedUpdate()
    {
        if (sliding)
        {
            slideTimer -= Time.deltaTime;
            slideMult = Mathf.Lerp(slideMult, 0, 0.02f);

            if(slideTimer<=0 && sliding)
            {
                UnSlide();
            }
        }
        movePlayer();
    }
    private void speedControl()
    {
        if (sliding) return;
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > baseSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * baseSpeed;
            if (!ground && Input.GetKey(KeyCode.W))
            {
                limitedVel = flatVel.normalized * baseSpeed * airmult;
            }
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpF, ForceMode.Impulse);
    }
    private void Slide()
    {
        slideSpeed = baseSpeed * slideMult * Time.fixedDeltaTime;
        Debug.Log(slideSpeed);
        rb.AddForce(moveDir * slideSpeed, ForceMode.Acceleration);
        sliding = true;
    }


    private void UnSlide()
    {
        sliding = false;
        slideTimer = slideTime;
        slideMult = 200f;
    }

    private void resetjump()
    {
        ready = true;
    }
}
