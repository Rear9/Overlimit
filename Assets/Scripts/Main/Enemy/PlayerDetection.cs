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
        player = GameObject.FindWithTag("Player");
        shootTime = shootTimer;
    }
    private void Update()
    {
        direction = (player.transform.position - transform.position).normalized;
        playerDetect = Physics.Raycast(transform.position,direction, out cast, castRange);
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
        Vector3 finalDirection = Quaternion.Euler(Random.Range(-2,0), Random.Range(-3,3), 0) * direction;
        GameObject shot = Instantiate(bullet, transform.position+transform.up*1f+transform.right*-.3f, Quaternion.Euler(finalDirection));
        shot.GetComponent<Rigidbody>().AddRelativeForce(finalDirection*shootSpeed,ForceMode.Impulse);
        shootTime = shootTimer;
    }
}
