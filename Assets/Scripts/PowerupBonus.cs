using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBonus : Powerup
{
    // INHERITANCE

    [SerializeField] float points = 10;
    

    // POLYMORPHISM
    protected override void Start()
    {
        // Change the bonus points based on the difficulty
        points *= GameManager.Instance.DifficultyMode;
        
        
        base.Start();
    }

    // POLYMORPHISM
    protected override void OnTriggerEnter2D(Collider2D collision)
    {   
        Gameplay.Instance.UpdateScore(((int)points));
        
        
        base.OnTriggerEnter2D(collision);
    }
}
