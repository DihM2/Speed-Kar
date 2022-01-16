using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //float horizontalInput;
    //float nPosX;


    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
