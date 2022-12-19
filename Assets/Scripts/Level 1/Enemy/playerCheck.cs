using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCheck : MonoBehaviour
{
    public Slowmo slowmo;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("head"))
        {
            Destroy(gameObject);
            slowmo.UnSlow();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if(collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}

