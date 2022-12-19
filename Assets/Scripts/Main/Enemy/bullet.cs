using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject prefab;
    public Transform orientation;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject g = Instantiate(prefab, Camera.main.transform.position + orientation.forward, Camera.main.transform.rotation);

            g.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 20f, ForceMode.Impulse);
        }
    }
}
