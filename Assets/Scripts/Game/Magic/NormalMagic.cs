using UnityEngine;

public class NormalMagic : MagicBase
{

    public override void Hit()
    {
        this.gameObject.SetActive(false);
    }

    public override void Move()
    {
        rb.velocity = move_direction * speed;
        Debug.Log(rb.velocity);
    }
}
