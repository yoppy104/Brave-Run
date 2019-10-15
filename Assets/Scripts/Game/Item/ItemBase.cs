using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// アイテムクラスの基礎になるクラス
public class ItemBase : MonoBehaviour
{
    // アイテムの種類
    public enum ItemType
    {
        HEAL,   // HP回復 
        CHARGE, // MP回復
        BULLET, // 特殊弾変更
        ENHANCE // 攻撃力上昇
    }

    // アイテムの種類
    [SerializeField] ItemType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ConstNumbers.TAG_NAME_PLAYER))
        {
            this.gameObject.SetActive(false);
        }
    }
}
