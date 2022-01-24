using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    float speed = 2f;
    float repeatWidth;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        repeatWidth = collider.size.y / 2;
        collider.enabled = false;

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

        if (transform.position.y < startPos.y - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
