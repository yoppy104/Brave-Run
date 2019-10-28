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
                Move(2);
                ChangeEnemyMode(3);
                Resetframe();
                break;
            case 3:
                if (Getattackframe() / 3 < Getmodeframe())
                {
                    E_anim.SetBool("isAttack", false);
                    ChangeEnemyMode(4);
                }
                break;
            case 4:
                Move(4);
                if (Getattackframe() == Getmodeframe())
                {
                    ChangeEnemyMode(1);
                }
                break;

        }

        if(Mathf.Sqrt(dx * dx + dy * dy) < 1.3)
        {
            AddTriggerframe();
        }else
        {
            ResetATriggerframe();
        }

        if (GetTriggerframe() == 1 && GetEnemymode() == 3)
        {
            Debug.Log("ゆうしゃにこうげきしたよ by flyableenemy");
            Attack();
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

}
