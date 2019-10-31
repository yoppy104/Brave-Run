using UnityEngine;

/// <summary>
/// ステージの挙動を制御する
/// </summary>
public class StageMove : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 move_direction = new Vector3(-1f, 0f, 0f);

    private int count = 0;

    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.StartBGM(SoundManager.BGMType.GAME);

        ScoreManager.Instance.Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += move_direction * speed;

        count++;

        if (count % 500 == 0)
        {
            speed += 0.08f;
        }
    }
}
