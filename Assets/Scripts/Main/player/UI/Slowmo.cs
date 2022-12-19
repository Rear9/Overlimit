using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowmo : MonoBehaviour
{
    public bool slow;
    public float cutDistance;
    public float slowInc, slowFinal;
    public GameObject fracBullet, fracEnemy;

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
        GameObject brokenObj;
        if (Physics.Raycast(ray, out hit, cutDistance))
        {
            if (hit.collider.gameObject.CompareTag("Bullet"))
            {
                brokenObj = fracBullet;
            }
            else if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                brokenObj = fracEnemy;
            }
            else
            {
                brokenObj = null;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!brokenObj) return;
                Vector3 breakVel = hit.rigidbody.velocity;
                Vector3 breakPos = hit.collider.gameObject.transform.position;
                Quaternion breakRot = hit.collider.gameObject.transform.rotation;
                Debug.Log(breakVel + " " + breakPos + " " + breakRot);
                hit.collider.gameObject.SetActive(false);
                GameObject fracObj = Instantiate(brokenObj, breakPos, breakRot);
                foreach (Transform part in fracObj.transform)
                {
                    var rb = part.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = breakVel;
                        rb.AddExplosionForce(Random.Range(1, 1.75f), hit.point, 2);
                        Destroy(fracObj, 2);
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
