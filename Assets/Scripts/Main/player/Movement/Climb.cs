using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    [Header("Ref")]
    public movement movement;
    public Transform orient;
    public Rigidbody rb;
    public LayerMask wall;
    [Header("Climbs")]
    public float climbSpeed, maxClimbTime;
    private float climbTimer;
    public bool climbing;
    public int climbJumps;
    private int climbJumpsRemain;
    [Header("Detect")]
    public float detectLength, castRadius, maxLookAngle;
    private float lookAngle;
    private Transform lastWall;
    private Vector3 lastWallNorm;
    public float minAngleChange;
    [Header("Exiting")]
    public bool exitingWall;
    public float maxExitTime;
    private float exitTimer;

    private RaycastHit wallHit;
    private bool wallFront;

    private void Update()
    {
        checkWall();
        State();
        if (climbing && !exitingWall) climbMove();
    }

    private void State()
    {
        if(wallFront&&Input.GetKey(KeyCode.W) && lookAngle < maxLookAngle && !exitingWall && rb.velocity.y >= -1)
        {
            if (!climbing && climbTimer > 0) startClimb();
            if (Input.GetKeyDown(KeyCode.Space) && climbJumpsRemain>0) climbJump();
            if (climbTimer > 0) climbTimer -= Time.deltaTime;
            else if (climbTimer <= 0) stopClimb();
        }

        else if (exitingWall)
        {
            if (climbing) stopClimb();
            if(exitTimer > 0) exitTimer -= Time.deltaTime;
            else if (exitTimer <= 0) exitingWall = false;

        }

        else
        {
            if (climbing) stopClimb();
        }
    }

    private void checkWall()
    {
        bool newWall = wallHit.transform != lastWall || Mathf.Abs(Vector3.Angle(lastWallNorm, wallHit.normal)) > minAngleChange;
        wallFront = Physics.SphereCast(orient.position, castRadius, orient.forward, out wallHit, detectLength, wall);
        Debug.DrawRay(orient.position,orient.forward);
        lookAngle = Vector3.Angle(orient.forward, -wallHit.normal);

        if (wallFront && newWall || movement.ground)
        {
            climbTimer = maxClimbTime;
            climbJumpsRemain = climbJumps;
        }
    }
    private void startClimb()
    {
        lastWall = wallHit.transform;
        lastWallNorm = wallHit.normal;
        climbing = true;
    }
    private void climbMove()
    {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
    }
    private void stopClimb()
    {
        climbing = false;
    }

    private void climbJump()
    {
        climbing = false;
        Vector3 forceApply = transform.up * 8 + wallHit.normal * 4;
        rb.velocity = Vector3.zero;
        exitingWall = true;
        exitTimer = maxExitTime;
        rb.AddForce(forceApply, ForceMode.Impulse);
        climbJumpsRemain--;
    }
}
