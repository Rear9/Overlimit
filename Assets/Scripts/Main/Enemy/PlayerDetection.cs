using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public float castRange, distance, shootSpeed;
    private GameObject player;
    public GameObject bullet;
    private bool playerDetect;
    private RaycastHit cast;
    private float shootTime, shootTimer = 1f;
    private Vector3 direction;

    private void Start()
    {
        player = GameObject.FindWithTag("PlayerBase");
        shootTime = shootTimer;
    }
    private void Update()
    {
        direction = (player.transform.position - transform.position).normalized;
        playerDetect = Physics.Raycast(transform.position, direction, out cast, castRange);
        distance = Vector3.Distance(player.transform.position, transform.position);
        Quaternion toRotate = Quaternion.LookRotation(direction);
        
        if(playerDetect && player.transform.position.y < transform.position.y * 2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotate, 3 * Time.deltaTime);
        }



        if (shootTime > 0) shootTime -= Time.deltaTime;
        if (playerDetect && shootTime <= 0 && distance < castRange)
        {
            ShootTowardsPlayer();
        }
    }

    private void ShootTowardsPlayer()
    {
        GameObject shot = Instantiate(bullet, transform.position+transform.up*.5f+transform.right*.25f, Quaternion.Euler(direction));
        shot.GetComponent<Rigidbody>().AddRelativeForce(direction*shootSpeed,ForceMode.Impulse);
        shootTime = shootTimer;
    }
}
