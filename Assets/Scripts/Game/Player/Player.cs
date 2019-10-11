using UnityEngine;

public class Player : MonoBehaviour
{
    //最大体力
    [SerializeField] private int hp_max;

    //最大MP
    [SerializeField] private int mp_max;

    //攻撃力
    [SerializeField] private int attack;

    //魔術通常弾のプレハブ
    [SerializeField] private Object magic_prefab;

    //魔術オブジェクト
    private GameObject magic_object;

    //魔術スクリプト
    private MagicBase magic_script;

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

    public MagicBase MagicScript
    {
        get { return this.magic_script; }
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
        this.hp = Mathf.Clamp(this.hp + delta, 0, hp_max);
    }

    //範囲を超えないようにMpを増減させる
    public void PlusMp(int delta)
    {
        this.mp = Mathf.Clamp(this.mp + delta, 0, mp_max);
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = hp_max;
        mp = mp_max;

        magic_object = Instantiate(magic_prefab) as GameObject;
        magic_script = magic_object.GetComponent<MagicBase>();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_GAME_AREA))
        {
            Debug.Log("Game Over...");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_GOAL_AREA))
        {
            Debug.Log("Game Clear!!!");
        }
    }
}
