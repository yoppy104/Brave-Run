using UnityEngine;

public class PenetrateBulletItem : ItemBase
{

    [SerializeField] private Object prefab;

    private static GameObject bullet;

    public override void UseEffect(Player player)
    {
        player.SetSpecialMagic(bullet);
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
