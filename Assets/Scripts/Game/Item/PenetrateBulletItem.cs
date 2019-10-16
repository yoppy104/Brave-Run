using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateBulletItem : ItemBase
{

    [SerializeField] private Object prefab;

    private static GameObject bullet;

    public override void UseEffect(Player player)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (bullet == null)
        {
            bullet = Instantiate(prefab) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
