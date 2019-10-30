using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enum EnemyType
    {
        GRAND,
        FLY
    }

    //プレハブ
    [SerializeField] private Object grand_enemy_prefab;
    [SerializeField] private Object fly_enemy_prefab;

    //敵リスト
    private List<GameObject> enemies;

    

    //敵の再生成処理
    private void Respawn(EnemyType type)
    {
        bool is_fly = false;
        switch (type)
        {
            case EnemyType.FLY:
                is_fly = true;
                break;
            default:
                is_fly = false;
                break;
        }

        GameObject respawn_obj = null;
        foreach(GameObject obj in enemies)
        {
            FlyableEnemy script = obj.GetComponent<FlyableEnemy>();
            if (!obj.activeSelf && (script != null) && is_fly)
            {
                respawn_obj = obj;
                break;
            }
            else if (!obj.activeSelf && (script == null) && !is_fly)
            {
                respawn_obj = obj;
                break;
            }
        }

        //再生成オブジェクトが存在するならアクティブ状態にして、終了
        if (respawn_obj != null)
        {
            respawn_obj.SetActive(true);
        }
        //存在しないなら、追加で生成
        else
        {
            AddEnemy(type);
        }
    }

    //敵を新しく追加する処理
    public void AddEnemy(EnemyType type)
    {
        Object prefab = null;
        switch (type)
        {
            case EnemyType.FLY:
                prefab = fly_enemy_prefab;
                break;
            case EnemyType.GRAND:
                prefab = grand_enemy_prefab;
                break;
        }
        if (prefab != null)
        {
            enemies.Add(Instantiate(prefab) as GameObject);
        }
    }

    //全ての敵オブジェクトを非アクティブにする。
    public void AllDisactiveEnemy()
    {
        foreach(GameObject obj in enemies)
        {
            obj.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
