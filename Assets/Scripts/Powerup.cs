using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Unit
{
    // INHERITANCE

    // POLYMORPHISM
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // special effects of the powerup
        // coin = score + 10
        // diamond = score + 50
        // gas = gas + 30
        Destroy(gameObject);
    }
}
