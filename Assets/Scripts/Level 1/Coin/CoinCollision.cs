using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    public Score score;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            score.score++;
            score.coinpickup.pitch = Random.Range(0.95f, 1.05f);
            score.coinpickup.Play();
        }
    }
}
