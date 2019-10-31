using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject arm;

    //魔術オブジェクト
    private GameObject magic_object;

    private GameObject special_magic_object;

    //魔術スクリプト
    private MagicBase magic_script;

    private MagicBase special_magic_script;

    //現在体力
    private int hp;

    //現在MP
    private int mp;

    //UIを管理するクラス
    private PlayerUI ui;

    //現在のスコア
    private int score;

    //残りの走行距離
    private int distance;

    private PlayerAnimator p_anim;

    protected int scenechange;
    protected float timecount;
    protected GameObject cam;
    protected CameraControll camSprite;

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

    public MagicBase SpecialMagicScript
    {
        get { return this.special_magic_script; }
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

    public int Score
    {
        get { return this.score; }
    }

    public int Distance
    {
        get { return this.distance; }
    }

    public void MessageUseSpecialBullet()
    {
        ui.SetSpecailNum(special_magic_script.UseLimit - special_magic_script.UseNum);
    }

    public void Damage(int value)
    {
        this.hp = Mathf.Clamp(this.hp - value, 0, hp_max);
        ui.SetHP(hp);
        p_anim.SetDamageAnimation();

        if (hp <= 0)
        {
            SceneManager.LoadScene(ConstNumbers.SCENE_NAME_GAMEOVER);
        }
    }

    //範囲を超えないように体力を増減させる。
    public void PlusHp(int delta)
    {
        this.hp = Mathf.Clamp(this.hp + delta, 0, hp_max);
        ui.SetHP(hp);
    }

    //範囲を超えないようにMpを増減させる
    public void PlusMp(int delta)
    {
        this.mp = Mathf.Clamp(this.mp + delta, 0, mp_max);
        ui.SetMP(mp);
    }

    public void plusScore(int value)
    {
        this.score += value;
        ui.SetScore(score);
    }

    public void PlusDistance(int value)
    {
        this.distance += value;
        ui.SetDistance(distance);
    }

    public void SetSpecialMagic(GameObject bullet)
    {
        this.special_magic_object = bullet;
        this.special_magic_script = bullet.GetComponent<MagicBase>();

        this.special_magic_script.UseNum = 0;

        ui.SetSpecailBulletInfo(special_magic_script.Type, special_magic_script.UseLimit);
    }

    public void CheckEraceSpecialBullet()
    {
        if (!this.special_magic_script.IsUseable())
        {
            this.special_magic_script = null;
            this.special_magic_object = null;

            ui.SetSpecailBulletInfo(MagicBase.MagicType.NORMAL, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = hp_max;
        mp = mp_max;

        magic_object = Instantiate(magic_prefab) as GameObject;
        magic_script = magic_object.GetComponent<MagicBase>();

        ui = GetComponent<PlayerUI>();

        //scoreとUIの初期化
        score = 0;

        ui.SetScore(score);

        //distanceとUIの初期化
        distance = 0;

        ui.SetDistance(distance);

        p_anim = gameObject.GetComponent<PlayerAnimator>();

        scenechange = 0;
        timecount = 2f;
        cam = GameObject.Find("Main Camera");
        camSprite = cam.GetComponent<CameraControll>();
    }

    // 腕を動かす処理
    private void MoveArm()
    {
        Vector3 from = arm.transform.position + arm.transform.forward;
        Vector3 to = InputManager.BeamPoint() - arm.transform.position;

        float angle = Vector3.SignedAngle(from, to, Vector3.forward);

        arm.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        MoveArm();
        if (scenechange==1)
        {
            timecount -= Time.deltaTime;
            camSprite.Goal();
            if (timecount < 0)
            {
                SceneManager.LoadScene(ConstNumbers.SCENE_NAME_CLEAR);
            }
        }
        if (scenechange == 2)
        {
            SceneManager.LoadScene(ConstNumbers.SCENE_NAME_GAMEOVER);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_GAME_AREA))
        {
            scenechange = 2;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject obj = collision.gameObject;

        // クリア範囲に入ったらゲームクリア
        if (obj.CompareTag(ConstNumbers.TAG_NAME_GOAL_AREA))
        {
            scenechange = 1;
        }

        // アイテムと接触したら効果処理を呼び出す。
        else if (obj.CompareTag(ConstNumbers.TAG_NAME_ITEM))
        {
            ItemBase script = obj.GetComponent<ItemBase>();
            script.UseEffect(this);
        }
    }
}
