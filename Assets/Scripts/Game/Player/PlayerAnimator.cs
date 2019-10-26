using System.Collections;
using UnityEngine;

/// <summary>
/// playerのアニメーションを制御するクラス
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{

    Animator anim;

    //ジャンプアニメーションを制御
    public void SetJumpAnimation(bool trigger)
    {
        anim.SetBool(ConstNumbers.ANIMATION_FLAG_NAME_JUMP, trigger);
    }

    //ダメージアニメーションを制御
    public void SetDamageAnimation(bool trigger)
    {
        anim.SetBool(ConstNumbers.ANIMATION_FLAG_NAME_DAMAGE, trigger);
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
}
