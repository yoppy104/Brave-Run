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

    private Player player;
    
    //ジャンプ処理
    public void Jump()
    {
        if (count_jamp < MAX_COUNT_OF_JAMP)
        {
            rb.velocity = new Vector2(0, JAMP_POWER);
            count_jamp++;
        }
        is_jamp = false;
    }

    public void Beam(Vector3 position)
    {
        MagicBase script = player.MagicScript;

        if (!script.gameObject.activeSelf)
        {
            script.gameObject.transform.position = transform.position - transform.right*0.5f - transform.forward*0.5f;

            script.MoveStart(position);
        }
    }


    // コンストラクタ
    void Start()
    {
        player = GetComponent<Player>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update処理
    void Update()
    {
        if (InputManager.JampInput())
        {
            is_jamp = true;
        }

        if (InputManager.BeamInput())
        {
            Beam(InputManager.BeamPoint());
        }
    }

    //物理演算の処理
    private void FixedUpdate()
    {
        if (is_jamp)
        {
            Jump();
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
