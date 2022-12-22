using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    public Score score;
    public Timer timer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            score.score++;
            timer.time--;
            score.coinpickup.pitch = Random.Range(0.95f, 1.05f);
            score.coinpickup.Play();
        }
    }
}
