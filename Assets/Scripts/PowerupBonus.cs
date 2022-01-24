using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBonus : Powerup
{
    [SerializeField] float points = 10;
    //AudioSource bonusSound;

    protected override void Start()
    {
        // Change the bonus points based on the difficulty
        points *= GameManager.Instance.DifficultyMode;
        
        
        base.Start();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {   
        Gameplay.Instance.UpdateScore(((int)points));
        
        
        base.OnTriggerEnter2D(collision);
    }
}
