using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : Unit
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        //Debug.Log("You Crashed! Game Over!");
        Gameplay.Instance.GameOver("You Crashed!");
        //call the game over method
    }
}
