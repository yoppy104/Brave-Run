using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //最大体力
    [SerializeField] private int hp_max;

    //攻撃力
    [SerializeField] private int attack;

    public int HpMax
    {
        get { return this.hp_max; }
    }

    public int Hp { get; set; }

    public int ATK
    {
        get { return this.attack; }
    }

    private float Vx { get; set; }
    private float Vy { get; set; }

    private GameObject player;
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

        Vx = 0;
        Vy = 0;

    }
    
    public void AddVx(float f)
    {
        Vx += f;
        if(Vx>0&&f<0|| Vx < 0 && f > 0)
        {
            Vx += 10*f;
        }
    }

    public void AddVy(float f)
    {
        Vy += f;
        if (Vy > 0 && f < 0 || Vy < 0 && f > 0)
        {
            Vy += 10*f;
        }
    }

    public void Move()
    {
        E_transform.Translate(new Vector3(Vx, Vy));
    }

    public void Attack()
    {
        p.PlusHp(-1 * ATK);
    }

    public void Damage(int damage)
    {
        Hp -= damage;
        if (Hp < 0)
        {
            Destroy(this);
        }
    }
}
