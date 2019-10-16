using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreate : MonoBehaviour
{
    // プレハブ情報はインスペクタから持たせる
    [SerializeField] private Object[] item_prefabs;

    // アイテムを再生成するまでの時間
    [SerializeField] private float time_recreate_item;

    // オブジェクトは連想配列で管理する
    private Dictionary<ItemBase.ItemType, GameObject> items;

    // Start is called before the first frame update
    void Start()
    {
        items = new Dictionary<ItemBase.ItemType, GameObject>();

        Transform parent = this.transform;

        foreach (GameObject obj in item_prefabs)
        {
            // インスタンスはステージオブジェクトの子供として持たせる
            GameObject temp = Instantiate(obj) as GameObject;
            temp.transform.parent = parent;
            items[obj.GetComponent<ItemBase>().Type] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // アイテムの種類をランダムに選択する
    private GameObject RandomChoiceItem()
    {
        float random = Random.Range(0, item_prefabs.Length);
        switch (random)
        {
            case 0:
                return items[ItemBase.ItemType.CHARGE];
            case 1:
                return items[ItemBase.ItemType.ENHANCE];
            case 2:
                return items[ItemBase.ItemType.HEAL];
            case 3:
                return items[ItemBase.ItemType.PENETRATION];
            case 4:
                return items[ItemBase.ItemType.SCATTERED];
            default:
                return null;
        }
    }

    // 渡されたアイテムの座標をランダムに選択する。
    private void SetPosition(GameObject obj)
    {
    }

    private bool is_running = false;
    private IEnumerator Create()
    {
        if (is_running)
        {
            yield break;
        }
        else
        {
            is_running = true;
        }

        float wait_time = Random.Range(-3, 3) + time_recreate_item;

        yield return new WaitForSeconds(wait_time);

        GameObject use_item = RandomChoiceItem();
        if (use_item == null)
        {
            Debug.LogError("アイテム生成でNullが返却されました");
            yield break;
        }


        is_running = false;
    }
}
