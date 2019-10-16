using UnityEngine;

/// <summary>
/// 散弾
/// </summary>
public class ScatteredMagic : MagicBase
{
    [SerializeField] private Object normal_magic_prefab;

    private static GameObject[] magics;

    private const int num_magic_bullet = 6;

    private bool is_scattered = false;

    //散弾処理
    private void CreateAroundMagic()
    {
        is_scattered = true;

        Vector3 front = move_direction;
        Vector3 right = new Vector3(front.y, -1 * front.x, 0f);
        Vector3 left = right * -1;
        Vector3 right_for = new Vector3(
            (front.x * Mathf.Cos(Mathf.PI / 4) + front.y * Mathf.Sin(Mathf.PI / 4)),
            (front.x * -1 * Mathf.Sin(Mathf.PI / 4) + front.y * Mathf.Cos(Mathf.PI / 4)),
            0f
            );
        Vector3 left_for = new Vector3(
            (front.x * Mathf.Cos(-1 * Mathf.PI / 4) + front.y * Mathf.Sin(-1 * Mathf.PI / 4)),
            (front.x * -1 * Mathf.Sin(-1 * Mathf.PI / 4) + front.y * Mathf.Cos(-1 * Mathf.PI / 4)),
            0f
            );
        Vector3 right_back = left_for * -1;
        Vector3 left_back = right_for * -1;

        Vector3[] points = new Vector3[num_magic_bullet] { right, left, right_for, left_for, right_back, left_back };

        //すべてのオブジェクトに初期座標と移動方向を与える。
       for (int i = 0; i < num_magic_bullet; i++)
        {
            magics[i].transform.position = transform.position + points[i];
            magics[i].GetComponent<NormalMagic>().MoveStart(transform.position + points[i] * 2);
            magics[i].GetComponent<NormalMagic>().Speed = this.speed;
            magics[i].SetActive(true);
        }
    }

    public override void MoveStart(Vector3 target)
    {
        base.MoveStart(target);

        is_scattered = false;

        //弾丸オブジェクトを生成する
        if (magics == null)
        {
            magics = new GameObject[num_magic_bullet];
            for (int i = 0; i < num_magic_bullet; i++)
            {
                magics[i] = Instantiate(normal_magic_prefab) as GameObject;
            }
        }
    }

    public override void Move()
    {
        base.Move();

        // 射撃入力がされたら、散弾処理を呼び出す
        if (InputManager.SpecialBeamInput())
        {
            if (!is_scattered)
            {
                CreateAroundMagic();
            }
        }
    }

    public override void Hit()
    {
        if (!is_scattered)
        {
            CreateAroundMagic();
        }
        gameObject.SetActive(false);
    }
}
