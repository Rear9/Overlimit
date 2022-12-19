using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wallrun : MonoBehaviour
{
    public Transform orientation;
    public float wallDist = .5f, minimumJumpH = 1.5f, wallUpFactor = 10f, exitTime, exitTimer = .5f, wallJumpForce, camTilt, camTiltTime, wallSpeed;
    public float Tilt { get; private set; }
    public LayerMask wallCheck;

    private bool _lWall, _rWall, exiting = false;
    public bool wallrunning;
    private Rigidbody rb;
    private RaycastHit _lWallHit, _rWallHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    bool CanWallrun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpH);
    }

    void FindWall()
    {
        _lWall = Physics.Raycast(transform.position, -orientation.right, out _lWallHit, wallDist, wallCheck);
        _rWall = Physics.Raycast(transform.position, orientation.right, out _rWallHit, wallDist, wallCheck);
    }

    private void Update()
    {
        FindWall();

        if (exiting)
        {
            if (exitTime > 0)
            {
                exitTime -= Time.deltaTime;
            }
            if (exitTime <= 0)
            {
                exiting = false;
            }
        }

        if (CanWallrun() && !exiting)
        {
            if (_lWall)
            {
                WallRun();
            }
            else if (_rWall)
            {
                WallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            if (wallrunning)
            {
                wallrunning = false;
            }
            StopWallRun();
        }
    }

    void WallRun()
    {
        wallrunning = true;
        rb.AddForce(orientation.forward * wallSpeed, ForceMode.Acceleration);

        if (_lWall)
        {
            Tilt = Mathf.Lerp(Tilt, -camTilt, camTiltTime * Mathf.Sin(Time.deltaTime));
        }
        else if (_rWall)
        {
            Tilt = Mathf.Lerp(Tilt, camTilt, camTiltTime * Mathf.Sin(Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            exiting = true;
            exitTime = exitTimer;
            if (_lWall)
            {
                Vector3 jumpDirection = transform.up * wallUpFactor + _lWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(jumpDirection * wallJumpForce, ForceMode.Impulse);
                rb.AddForce(orientation.forward * wallSpeed/2, ForceMode.Acceleration);
            }
            else if (_rWall)
            {
                Vector3 jumpDirection = transform.up * wallUpFactor + _rWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
                rb.AddForce(jumpDirection * wallJumpForce, ForceMode.Impulse);
                rb.AddForce(orientation.forward * wallSpeed, ForceMode.Acceleration);
            }
        }
    }

    void StopWallRun()
    {
        Tilt = Mathf.Lerp(Tilt,0,camTiltTime * Time.deltaTime);
    }
}
