using UnityEngine;

public class Player : MonoBehaviour
{
    //最大体力
    [SerializeField] private int hp_max;

    //最大MP
    [SerializeField] private int mp_max;

    //攻撃力
    [SerializeField] private int attack;


    //現在体力
    private int hp;

    //現在MP
    private int mp;


    public int HpMax
    {
        get { return this.hp_max; }
    }

    public int MpMax
    {
        get { return this.mp_max; }
    }

    public int Hp
    {
        get { return this.hp; }
        set { this.hp = value; }
    }

    public int Mp
    {
        get { return this.mp; }
        set { this.mp = value; }
    }

    public int Attack
    {
        get { return this.attack; }
    }

    //範囲を超えないように体力を増減させる。
    public void PlusHp(int delta)
    {
        this.hp += delta;
        if (this.hp > this.hp_max)
        {
            this.hp = this.hp_max;
        }
        else if (this.hp < 0)
        {
            this.hp = 0;
        }
    }

    //範囲を超えないようにMpを増減させる
    public void PlusMp(int delta)
    {
        this.mp += delta;
        if (this.mp > this.mp_max)
        {
            this.mp = this.mp_max;
        }
        else if (this.mp < 0)
        {
            this.mp = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = hp_max;
        mp = mp_max;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
