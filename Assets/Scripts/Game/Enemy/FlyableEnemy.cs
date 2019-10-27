using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyableEnemy : Enemy
{
    private float dx;
    private float dy;
    // Update is called once per frame
    void Update()
    {
        Addframe();
        dx = E_transform.position.x - P_transform.position.x;
        dy = E_transform.position.y - P_transform.position.y;
        switch (GetEnemymode())
        {
            case 0:
                if (Getwaitframe() == Getmodeframe())
                {
                    ChangeEnemyMode(1);
                }
                Move(0);
                break;
            case 1:
                if (Mathf.Sqrt(dx * dx + dy * dy) < 2.5)
                {
                    ChangeEnemyMode(2);
                }
                Move(1);
                break;
            case 2:
                Debug.Log("甲でき");
                Move(2);
                ChangeEnemyMode(3);
                Resetframe();
                break;
            case 3:
                if(Getattackframe() == Getmodeframe())
                {
                    ChangeEnemyMode(1);
                }
                if (Getattackframe() / 3 < Getmodeframe())
                {
                    E_anim.SetBool("isAttack", false);
                    Move(4);
                }
                break;

        }

    }

    public void ChangeEnemyMode(int mode)
    {
        enemymode = mode;
        switch (mode)
        {
            case 1:
                E_anim.SetBool("isAttack", false);
                break;
            case 2:
                E_anim.SetBool("isAttack", true);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Attack();
        }
    }

}
