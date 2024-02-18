using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Variables related to enemy character movement
    public float enemySpeed;
    Rigidbody2D enemyRb2D;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    // FixedUpdate has the same call rate as the physics system
    private void FixedUpdate()
    {
        Vector2 position = enemyRb2D.position;

        if (vertical)
        {
            position.y = position.y + enemySpeed * direction * Time.deltaTime;
        }
        else
        {
            position.x = position.x + enemySpeed * direction * Time.deltaTime;
        }
        enemyRb2D.MovePosition(position);
    }
}
