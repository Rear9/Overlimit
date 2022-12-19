using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource sound;
    public movement movement;

    void Update()
    {
        if (movement.ground && movement.state == movement.State.Sprint)
        {
            sound.enabled = true;
            sound.loop = true;
        }
        else
        {
            sound.enabled = false;
        }
    }
}
