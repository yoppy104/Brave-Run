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

        float y_pos = Random.Range(-5f, 5f);
        //再生成オブジェクトが存在するならアクティブ状態にして、終了
        if (respawn_obj != null)
        {
            respawn_obj.SetActive(true);
            respawn_obj.transform.position = Camera.main.transform.position + new Vector3(-10f, y_pos, 10f);
        }
        //存在しないなら、追加で生成
        else
        {
            AddEnemy(type);
            enemies[enemies.Count - 1].transform.position = Camera.main.transform.position + new Vector3(-10f, y_pos, 10f);
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
                //prefab = grand_enemy_prefab;
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

    private int wait_count = 0;
    private int levelup_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wait_count > 100)
        {
            if (CountActiveEnemy() < 5)
            {
                StartCoroutine(RespawnEnemy());
            }

            levelup_count++;
            if (levelup_count % 600 == 0)
            {
                LevelUpEnemy();
            }
        }
        else { wait_count++; }
    }

    private int count_call = 0;
    private IEnumerator RespawnEnemy()
    {
        count_call++;
        if (count_call > 5 - CountActiveEnemy())
        {
            count_call--;
            yield break;
        }
        float random = Random.Range(0f, 5f);
        yield return new WaitForSeconds(random);

        Respawn(EnemyType.FLY);

        yield return null;

        count_call--;
    }

    private int CountActiveEnemy()
    {
        int count = 0;
        foreach(GameObject obj in enemies)
        {
            if (obj.activeSelf)
            {
                count++;
            }
        }
        return count;
    }

    private void LevelUpEnemy()
    {
        foreach(GameObject obj in enemies)
        {
            Enemy script = obj.GetComponent<Enemy>();
            script.Hp += 5;
            script.ATK += 2;
            script.Cooltime = Mathf.Clamp(script.Cooltime - 1, 1, 100);
            script.Waittime = Mathf.Clamp(script.Waittime - 1, 1, 100);
        }
    }
}
