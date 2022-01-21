using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int score;
    float fuel = 100f;

    float difficulty = 2f;

    GameplayUI gameplayUI;

    // Start is called before the first frame update
    void Start()
    {
        gameplayUI = GameObject.Find("UI Screen").GetComponent<GameplayUI>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(transform.position.x < 2)
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x > -2)
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y);
            }
        }

        UpdateFuel(- Time.deltaTime * difficulty);
    }

    public void UpdateScore(int value)
    {
        score += value;
        // update Score Text UI
        gameplayUI.ShowScore(score);
    }

    public void UpdateFuel(float value)
    {
        if((fuel + value) > 100)
        {
            fuel = 100;
        }
        else if((fuel + value) > -1)
        {
            fuel += value;
        }
        
        if(fuel < 0)
        {
            // Game over
            Debug.Log("You are out of gas! GAME OVER!");
            Time.timeScale = 0;
        }
        // Update gas text UI
        gameplayUI.ShowFuel(fuel);
    }
}
