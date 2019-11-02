using UnityEngine;

public class NormalMagic : MagicBase
{
    public override void Hit()
    {
        this.gameObject.SetActive(false);
    }
}
