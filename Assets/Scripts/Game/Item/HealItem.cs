using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{

    [SerializeField] private int point;

    public override void UseEffect(Player player)
    {
        player.PlusHp(point);
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
