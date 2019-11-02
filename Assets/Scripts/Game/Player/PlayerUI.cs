using UnityEngine;
using UnityEngine.UI;
using System.Text;

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

    [SerializeField] private Transform position_goal;       //ゴール地点オブジェクト

    private StringBuilder distance_text_buffer;

    private StringBuilder score_text_buffer;

    private const string FILL_TEXT = "00000";
    private const int NUM_FILL = 5;

    private int pre_scene_distance_data = 1000000;

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


    public void SetUIData(int hp_max, int mp_max) {
        hp_bar.maxValue = hp_max;
        mp_bar.maxValue = mp_max;
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
        int index = (score_text_buffer.ToString()).Length - NUM_FILL;
        string replace_text = (score_text_buffer.ToString()).Substring(0, index);
        score_text_buffer = score_text_buffer.Replace(replace_text, value.ToString(FILL_TEXT));
        this.score_text.text = score_text_buffer.ToString();
    }

    //残距離を表示する。
    public void SetDistance(int value)
    {
        if (pre_scene_distance_data < value) { return; }

        pre_scene_distance_data = value;
        int index = (distance_text_buffer.ToString()).Length - 1;
        string replace_text = distance_text_buffer.ToString().Substring(0, index);
        distance_text_buffer = distance_text_buffer.Replace(replace_text, value.ToString());
        this.running_distance_text.text = distance_text_buffer.ToString();
    }

    void Awake()
    {
        score_text_buffer = new StringBuilder();
        score_text_buffer.Append(FILL_TEXT);
        score_text_buffer.Append("point");

        distance_text_buffer = new StringBuilder();
        distance_text_buffer.Append("100");
        distance_text_buffer.Append("m");
    }

    private float CalcDistance()
    {
        Vector3 pos = transform.position;
        Vector3 goal = position_goal.position;

        return Mathf.Abs(Vector3.Distance(pos, goal));
    }

    private void Update()
    {
        SetDistance((int)CalcDistance());
    }
}
