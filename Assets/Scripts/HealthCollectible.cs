using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int healthIncrease = 0;
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null && controller.health < controller.maxHealth)
        {
                controller.ChangeHealth(healthIncrease);
                controller.PlaySound(collectedClip);
                Destroy(gameObject);
        }

    }
}
