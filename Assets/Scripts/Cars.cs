using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : Unit
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Time.timeScale = 0;
        Debug.Log("Game Over!");

        //call the game over method
    }
}
