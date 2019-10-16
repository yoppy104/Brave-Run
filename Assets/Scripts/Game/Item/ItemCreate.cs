using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreate : MonoBehaviour
{
    // プレハブ情報はインスペクタから持たせる
    [SerializeField] private Object[] item_prefabs;

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
}
