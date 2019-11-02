using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //ジャンプの上方向への移動
    private const float jump_POWER = 500f;

    //ジャンプの最大回数
    private const int MAX_COUNT_OF_JUMP = 2;

    //ジャンプ入力があったか
    private bool is_jump = false;

    //ジャンプ回数
    private int count_jump = 0;

    private bool is_jump_now = false;

    //物理制御クラス
    private Rigidbody2D rb;

    private Player player;

    private PlayerAnimator p_anim;
    
    //ジャンプ処理
    public void Jump()
    {
        rb.velocity = new Vector2(0f, jump_POWER / 50f);
        is_jump = false;
        p_anim.SetJumpAnimation(true);
    }

    /// <summary>
    /// 通常弾を発射する処理
    /// </summary>
    /// <param name="position">発射時の目標地点</param>
    public void Beam(Vector3 position)
    {
        MagicBase script = player.MagicScript;

        if (player.Mp <= 0) return;

        if (!script.gameObject.activeSelf)
        {
            if (transform.position.x > position.x)
            {
                script.gameObject.transform.position = transform.position - transform.right * 0.5f - transform.forward * 0.5f;

                script.MoveStart(position);

                player.PlusMp(-script.Cost);
            }
        }
    }

    /// <summary>
    /// 特殊弾を発射するスクリプト
    /// </summary>
    /// <param name="position">発射時の目標地点</param>
    public void SpecialBeam(Vector3 position)
    {
        MagicBase script = player.SpecialMagicScript;
        if (script != null)
        {
            if (script.IsUseable() && !script.gameObject.activeSelf)
            {
                if (transform.position.x > position.x)
                {
                    script.gameObject.transform.position = transform.position - transform.right * 0.5f - transform.forward * 0.5f;

                    script.MoveStart(position);

                    script.UseNum++;

                    player.MessageUseSpecialBullet();

                    player.CheckEraceSpecialBullet();
                }
            }
        }
    }

    // コンストラクタ
    void Start()
    {
        player = GetComponent<Player>();

        rb = GetComponent<Rigidbody2D>();

        p_anim = gameObject.GetComponent<PlayerAnimator>();
    }

    // Update処理
    void Update()
    {
        //CheckTouchedGround();

        if (InputManager.JumpInput())
        {
            if (count_jump < MAX_COUNT_OF_JUMP)
            {
                is_jump = true;
            }
        }

        if (InputManager.BeamInput())
        {
            Beam(InputManager.BeamPoint());
        }
        if (InputManager.SpecialBeamInput())
        {
            SpecialBeam(InputManager.BeamPoint());
        }
    }

    //物理演算の処理
    private void FixedUpdate()
    {
        if (is_jump)
        {
            Jump();

            if (count_jump == 1)
            {
                count_jump++;
            }
        }

        rb.AddForce(Vector3.down * ConstNumbers.GRAVITY_POWER, ForceMode2D.Force);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_STAGE))
        {
            is_jump_now = true;
            count_jump++;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        //ステージに接触している間、それが自分よりも下側だったら、接地したとみなす。
        if (is_jump_now && collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_STAGE))
        {
            int num_hit_pos = 0;
            Vector2 hit_pos = Vector2.zero;
            foreach (ContactPoint2D point in collision.contacts)
            {
                hit_pos += point.point;
                num_hit_pos++;
            }
            hit_pos /= num_hit_pos;

            Vector3 p_pos = transform.position;

            if ((p_pos.y > hit_pos.y) && (p_pos.x - 0.3f < hit_pos.x && p_pos.x + 0.3 > hit_pos.x))
            {
                count_jump = 0;
                is_jump_now = false;
                p_anim.SetJumpAnimation(false);
            }
        }
    }
}
