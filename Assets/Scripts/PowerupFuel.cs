using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupFuel : Powerup
{
    // INHERITANCE

    [SerializeField] float fuelGallon = 20;

    // POLYMORPHISM
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        playerController.UpdateFuel(fuelGallon);

        base.OnTriggerEnter2D(collision);
    }
}
