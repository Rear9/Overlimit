using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    public Slowmo slowmo;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            slowmo.UnSlow();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Load try again UI
        }
    }
}
