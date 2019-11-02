using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeItem : ItemBase
{

    // 数値を変動させるポイント
    [SerializeField] int point;

    public override void UseEffect(Player player)
    {
        player.PlusMp(point);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
