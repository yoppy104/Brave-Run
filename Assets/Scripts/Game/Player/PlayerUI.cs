using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのUIを管理するクラス
/// </summary>
public class PlayerUI : MonoBehaviour
{

    //バー関連
    [SerializeField] private Slider hp_bar; //体力バー
    [SerializeField] private Slider mp_bar; //MPバー

    //テキスト関連
    [SerializeField] private Text score_text;               //得点表示
    [SerializeField] private Text running_distance_text;    //走行距離表示

    //特殊弾関連
    [SerializeField] private Text specail_bullet_num_text;  //特殊弾の残弾表示
    [SerializeField] private Image special_bullet_image;    //所持特殊弾の表示
    [SerializeField] private Sprite[] special_bullet_icons; //特殊弾のアイコンをそろえた配列

    //MagicTypeに対応した配列のインデックスを返す。
    private int CastMagicTypeToIndex(MagicBase.MagicType type)
    {
        switch (type)
        {
            case MagicBase.MagicType.NORMAL:
                return 1;
            case MagicBase.MagicType.PENETRATION:
                return 2;
            case MagicBase.MagicType.SCATTER:
                return 3;
            default:
                return 0;
        }
    }

    //HPゲージを設定する。
    public void SetHP(int value)
    {
        this.hp_bar.value = value;
    }

    //MPゲージを設定する。
    public void SetMP(int value)
    {
        this.mp_bar.value = value;
    }

    //特殊魔術の情報を設定する
    public void SetSpecailBulletInfo(MagicBase.MagicType type, int num)
    {
        int index = CastMagicTypeToIndex(type);
        this.special_bullet_image.sprite = special_bullet_icons[index];
        this.specail_bullet_num_text.text = num.ToString();
    }

    //特殊魔術の数量を設定する。
    public void SetSpecailNum(int num)
    {
        this.specail_bullet_num_text.text = num.ToString();
    }

    //スコアを設定する。
    public void SetScore(int value)
    {
        this.score_text.text = value.ToString();
    }

    //残距離を表示する。
    public void SetDistance(int value)
    {
        this.running_distance_text.text = value.ToString();
    }
}
