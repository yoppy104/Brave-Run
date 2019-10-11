using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyableEnemy : Enemy
{
    // Update is called once per frame
    void Update()
    {
        AddVx((P_transform.position.x - E_transform.position.x) / 7500);
        AddVy((P_transform.position.y - E_transform.position.y) / 7500);
        Move();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();
        }
    }
}
