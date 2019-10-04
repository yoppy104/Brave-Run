using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //ジャンプの上方向への移動
    private const float JAMP_POWER = 6f;

    //ジャンプの最大回数
    private const int MAX_COUNT_OF_JAMP = 2;

    //ジャンプ入力があったか
    private bool is_jamp = false;

    //ジャンプ回数
    private int count_jamp = 0;

    //物理制御クラス
    private Rigidbody2D rb;


    // コンストラクタ
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update処理
    void Update()
    {
        if (InputManager.JampInput())
        {
            is_jamp = true;

        }
    }

    //物理演算の処理
    private void FixedUpdate()
    {
        if (is_jamp)
        {
            if (count_jamp < MAX_COUNT_OF_JAMP)
            {
                rb.velocity = new Vector2(0, JAMP_POWER);
                count_jamp++;
            }
            is_jamp = false;
        }
    }

    //接触判定
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_STAGE))
        {
            if (count_jamp != 0)
            {
                count_jamp = 0;
            }
        }
    }
}
