using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : ItemBase
{

    [SerializeField] private int add_score;

    public override void UseEffect(Player player)
    {
        base.UseEffect(player);

        ScoreManager.Instance.addScore( add_score );

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
