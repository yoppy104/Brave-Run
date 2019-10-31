﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //ジャンプの上方向への移動
    private const float JAMP_POWER = 500f;

    //ジャンプの最大回数
    private const int MAX_COUNT_OF_JAMP = 2;

    //ジャンプ入力があったか
    private bool is_jamp = false;

    //ジャンプ回数
    private int count_jamp = 0;

    //物理制御クラス
    private Rigidbody2D rb;

    private Player player;

    private PlayerAnimator p_anim;
    
    //ジャンプ処理
    public void Jump()
    {
        rb.velocity = new Vector2(0f, JAMP_POWER / 50f);
        is_jamp = false;
        p_anim.SetJumpAnimation(true);
    }

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

    private void CheckTouchedGround()
    {
        Vector3 check_center = transform.position;
        Vector3 check_radius = new Vector3(0.5f, 4f, 100f);
        Collider2D col = Physics2D.OverlapBox(check_center, check_radius, 0);

        if (col != null)
        {
            if (col.gameObject.CompareTag(ConstNumbers.TAG_NAME_STAGE))
            {
                Debug.Log(count_jamp);
                count_jamp = 0;
                p_anim.SetJumpAnimation(false);
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

        if (InputManager.JampInput())
        {
            if (count_jamp < MAX_COUNT_OF_JAMP)
            {
                is_jamp = true;
                count_jamp++;
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
        if (is_jamp)
        {
            Jump();
        }

        rb.AddForce(Vector3.down * ConstNumbers.GRAVITY_POWER, ForceMode2D.Force);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_STAGE))
        {
            count_jamp = 0;
            p_anim.SetJumpAnimation(false);
        }
    }
}
