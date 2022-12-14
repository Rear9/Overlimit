using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowmo : MonoBehaviour
{
    public bool slow;
    public float cutDistance;
    public float slowInc, slowFinal;
    public GameObject fracBullet;

    private void OnDrawGizmos()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, cutDistance) && hit.collider.gameObject.CompareTag("Bullet"))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit.point, .075f);
        }
    }

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, cutDistance) && hit.collider.gameObject.CompareTag("Bullet"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 breakVel = hit.rigidbody.velocity;
                hit.collider.gameObject.SetActive(false);
                GameObject fracBull = Instantiate(fracBullet, hit.point, Quaternion.identity);
                foreach (Transform part in fracBull.transform)
                {
                    var rb = part.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = breakVel;
                        rb.AddExplosionForce(Random.Range(1, 2), hit.point, 2);
                        Destroy(fracBull, 2);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (slow)
            {
                UnSlow();
            }
            else
            {
                Slow();
            }
        }
    }
   
    public void Slow()
    {
        slow = true;
        Time.timeScale = slowFinal;
        Time.fixedDeltaTime = slowInc * Time.timeScale;
    }
    
    public void UnSlow()
    {
        slow = false;
        Time.timeScale = 1;
        Time.fixedDeltaTime = slowInc;
    }
}
