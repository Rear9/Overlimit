using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camhold : MonoBehaviour
{
    public Transform campos;
    void Update()
    {
        transform.position = campos.position;
    }
}
