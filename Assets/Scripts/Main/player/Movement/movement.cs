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
    private float baseSpeed, walkSpeed, sprintSpeed;
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
    public float sliderTime, sliderTimer, slideSpeed, slideHeight;
    private bool sliding;

    [Header("Drag")]
    public LayerMask floor;
    public bool ground;
    public float baseDrag, height;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode slideKey = KeyCode.C;

    public enum State
    {
        Walk,
        Sprint,
        Wallrunning,
        Climbing,
        Sliding,
        Air
    }

    private void StateHandler()
    {
        if (ground && Input.GetKey(sprintKey) && Input.GetAxis("Vertical") > 0.9 && Input.GetAxis("Horizontal") == 0)
        {
            state = State.Sprint;
            jumpF = sprintJumpF;
            baseSpeed = Mathf.Lerp(baseSpeed, sprintSpeed, .005f);
        }
        else if (ground)
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
        else
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
    }
    private void Start()
    {
        gameManager.fovSlider.value = PlayerPrefs.GetFloat("Fov");
        gameManager.sensSlider.value = PlayerPrefs.GetFloat("Sens");
        gameManager.volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        ready = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        slideHeight = gameObject.transform.localScale.y;
        Cursor.visible = false;
    }

    private void movePlayer()
    {
        if (climb.exitingWall) return;
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
        ground = Physics.SphereCast(transform.position, 0.3f, Vector3.down,out hit, height/2.5f, floor);
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
        movePlayer();
    }
    private void speedControl()
    {
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
    private void resetjump()
    {
        ready = true;
    }
}
