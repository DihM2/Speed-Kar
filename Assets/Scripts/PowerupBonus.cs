using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBonus : Powerup
{
    [SerializeField] int points = 10;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        playerController.UpdateScore(points);

        base.OnTriggerEnter2D(collision);
    }
}
