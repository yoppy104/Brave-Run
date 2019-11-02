using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{

    private enum Mode
    {
        ACTIVE,
        DISACTIVE
    }

    private Mode mode;

    [SerializeField] private Sprite active_image;
    [SerializeField] private Sprite disactive_image;

    [SerializeField] private Player player;

    private Dictionary<Mode, Sprite> sprites;

    private SpriteRenderer sprite_rend;

    // Start is called before the first frame update
    void Start()
    {
        sprite_rend = gameObject.GetComponent<SpriteRenderer>();

        sprites = new Dictionary<Mode, Sprite>();
        sprites[Mode.ACTIVE] = active_image;
        sprites[Mode.DISACTIVE] = disactive_image;

        mode = Mode.ACTIVE;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = InputManager.BeamPoint();

        float x = player.transform.position.x;
        if (x < transform.position.x)
        {
            if (mode == Mode.ACTIVE)
            {
                mode = Mode.DISACTIVE;
                sprite_rend.sprite = sprites[mode];
            }
        }
        else
        {
            if (mode == Mode.DISACTIVE)
            {
                mode = Mode.ACTIVE;
                sprite_rend.sprite = sprites[mode];
            }
        }
    }
}
