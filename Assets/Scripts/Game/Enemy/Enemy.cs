using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //最大体力
    [SerializeField] private int hp_max;

    //攻撃力
    [SerializeField] private int attack;

    [SerializeField] private int waittime;//生成されてからプレイヤーに向かうまでの時間　推奨　3とか

    [SerializeField] private int attackcooltime;//攻撃のインターばる　5以上推奨

    private int modeframe = 0;

    public int enemymode; //0:待機(直進)　1:playerに向かってく　2:攻撃　3:攻撃中 4:攻撃クールタイム

    private int Triggerframe;

    public void AddTriggerframe()
    {
        Triggerframe++;
    }

    public void ResetATriggerframe()
    {
        Triggerframe = 0;
    }

    public int GetTriggerframe()
    {
        return Triggerframe;
    }

    public int HpMax
    {
        get { return this.hp_max; }
    }

    public int Hp { get; set; }

    public int ATK
    {
        get { return this.attack; }
    }

    public Animator E_anim;

    public GameObject player;
    public Rigidbody2D E_rigidbody;
    public Player p;
    public Transform P_transform;

    public Transform E_transform;
    // Start is called before the first frame update
    public void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        p = player.GetComponent<Player>();
        P_transform = player.GetComponent<Transform>();
        E_transform = this.GetComponent<Transform>();
        E_rigidbody = this.GetComponent<Rigidbody2D>();
        E_anim = this.GetComponent<Animator>();

        Hp = hp_max;

    }
    

    public void Move(int mode)
    {
        switch (mode)
        {
            case 0:
                E_rigidbody.velocity = new Vector2(1, 0);
                break;
            case 1:
                E_rigidbody.AddForce(new Vector2((P_transform.position.x - E_transform.position.x) *0.5f, (P_transform.position.y - E_transform.position.y) * 0.5f));
                E_rigidbody.velocity = new Vector2(Mathf.Clamp(E_rigidbody.velocity.x, -1, 1), Mathf.Clamp(E_rigidbody.velocity.y, -1, 1));
                break;
            case 2:
                E_rigidbody.velocity = new Vector2((P_transform.position.x - E_transform.position.x) * 2, (P_transform.position.y - E_transform.position.y) * 2);
                break;
            case 3:
                E_rigidbody.AddForce(new Vector2((P_transform.position.x - E_transform.position.x) * 0.5f, 0));
                E_rigidbody.velocity = new Vector2(Mathf.Clamp(E_rigidbody.velocity.x, -1, 1), E_rigidbody.velocity.y);
                break;
            case 4:
                E_rigidbody.AddForce(new Vector2((-20 - E_transform.position.x) * 0.3f, (8 - E_transform.position.y) * 0.3f));
                E_rigidbody.velocity = new Vector2(Mathf.Clamp(E_rigidbody.velocity.x, -2, 2), Mathf.Clamp(E_rigidbody.velocity.y, -2, 2));
                break;
            case 5:
                E_rigidbody.AddForce(new Vector2((-20 - E_transform.position.x) * 0.3f, 0));
                E_rigidbody.velocity = new Vector2(Mathf.Clamp(E_rigidbody.velocity.x, -1, 1), E_rigidbody.velocity.y);
                break;
        }
    }

    public void Attack()
    {
        p.Damage(ATK);
    }

    public void Damage(int damage)
    {
        Hp -= damage;
        E_anim.SetTrigger("isDamage");
        if (Hp < 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    

    public int GetEnemymode()
    {
        return enemymode;
    }

    public int Getwaitframe()
    {
        return waittime * 60;
    }

    public void Addframe()
    {
        modeframe++;
        modeframe %= 2000000000;
    }

    public void Resetframe()
    {
        modeframe = 0;
    }

    public int Getmodeframe()
    {
        return modeframe;
    }

    public int Getattackframe()
    {
        return attackcooltime * 60;
    }
}
