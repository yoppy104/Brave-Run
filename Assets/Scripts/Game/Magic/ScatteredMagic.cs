using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 散弾
/// </summary>
public class ScatteredMagic : MagicBase
{
    [SerializeField] private Object normal_magic_prefab;

    private static List<GameObject> normal_magic_instances;

    private const int num_magic_bullet = 6;

    private bool is_scattered = false;


    private void SetInstance(GameObject[] result)
    {
        int index = 0;

        // 規定個数になるまで、生成済みの配列から非アクティブなオブジェクトを取り出す。
        foreach (GameObject obj in normal_magic_instances)
        {
            if (!obj.activeSelf)
            {
                result[index] = obj;
                index++;

                if(index == num_magic_bullet)
                {
                    break;
                }
            }
        }

        // 不足分を生成して、補う
        if (index < num_magic_bullet)
        {
            for (int i = index; i < num_magic_bullet; i++)
            {
                GameObject new_bullet = Instantiate(normal_magic_prefab) as GameObject;
                result[i] = new_bullet;
                normal_magic_instances.Add(new_bullet);
            }
        }
    }

    //散弾処理
    private void CreateAroundMagic()
    {
        is_scattered = true;

        GameObject[] magics = new GameObject[num_magic_bullet];

        SetInstance(magics);

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

        StartCoroutine(AfterScattered());
    }

    public override void MoveStart(Vector3 target)
    {
        base.MoveStart(target);

        is_scattered = false;

        normal_magic_instances = new List<GameObject>();
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

    public IEnumerator AfterScattered()
    {
        rb.velocity = Vector3.zero;

        yield return null;

        gameObject.SetActive(false);
    }
}
