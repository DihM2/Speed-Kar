using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float fuel = 100f;
    float gasUseSpeed = 2f;

    // How many roads adjacents to the central road
    int roadSize = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Set the fuel use based on the game difficulty
        // Easy: gasUseSpeed * 0.5; Normal: gasUseSpeed * 1; Hard: gasUseSpeed * 1.5; Very Hard: gasUseSpeed * 2;
        gasUseSpeed *= GameManager.Instance.DifficultyMode;

        
        //Gameplay.Instance.ShowGameMode(GameManager.Instance.difficultyName);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        // Gas use based on the time passed * difficultySpeed
        if (Gameplay.Instance.isGameStart)
        {
            UpdateFuel(-Time.deltaTime * gasUseSpeed);
        }
        
    }

    // Move the player to determined positions
    void MovePlayer()
    {
        if (Gameplay.Instance.isGameStart)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                int posX = (int)Math.Round(transform.position.x);
                if (posX < roadSize)
                {
                    transform.position = new Vector3(posX + 1, transform.position.y);
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                int posX = (int)Math.Round(transform.position.x);
                if (posX > -roadSize)
                {
                    transform.position = new Vector3(posX - 1, transform.position.y);
                }
            }
        }
    }


    // Update the fuel
    public void UpdateFuel(float value)
    {
        // Excess fuel above 100% becomes score bonus
        if ((fuel + value) > 100)
        {
            int bonusScore = ((int)(fuel + value)) - 100 ;
            Gameplay.Instance.UpdateScore(bonusScore);

            fuel = 100;
        }
        else if((fuel + value) > -1)
        {
            fuel += value;
        }
        
        if(fuel < 0)
        {
            // Game over
            //Debug.Log("You are out of gas! GAME OVER!");
            Gameplay.Instance.GameOver("You are out of gas!");
        }
        // Update gas text UI
        Gameplay.Instance.ShowFuel(fuel);
    }
}
