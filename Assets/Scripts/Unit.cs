using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // INHERITANCE

    float speed = 2f;

    // POLYMORPHISM
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Set the speed of the unit based on the game difficulty
        // Easy: speed * 0.5; Normal: speed * 1; Hard: speed * 1.5; Very Hard: speed * 2;
        speed *= GameManager.Instance.DifficultyMode;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gameplay.Instance.isGameStart)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if (transform.position.y < -4)
        {
            Destroy(gameObject);
        }
    }
}
