using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    int coinSmall = 1;
    int coinMedium = 5;
    int coinLarge = 10;

    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            if (gameObject.tag == "CoinSmall")
            {
                FindObjectOfType<GameSession>().AddToScore(coinSmall);
            }
            else if (gameObject.tag == "CoinMedium")
            {
                FindObjectOfType<GameSession>().AddToScore(coinMedium);
            }
            else 
            {
                FindObjectOfType<GameSession>().AddToScore(coinLarge);
            }
            
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
