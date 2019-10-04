using UnityEngine;

public class MagicBase : MonoBehaviour
{

    [SerializeField] protected int attack;

    [SerializeField] protected int cost;

    [SerializeField] protected int speed;

    protected Vector3 move_direction = Vector3.zero;

    protected Rigidbody2D rb;

    public int Attack
    {
        get { return this.attack;  }
    }

    public int Cost
    {
        get { return this.cost; }
    }

    public virtual void Hit()
    {

    }

    public virtual void Move()
    {

    }

    public virtual void MoveStart(Vector3 target)
    {
        move_direction = (target - transform.position).normalized;

        this.gameObject.SetActive(true);
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_PLAYER) || collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_GAME_AREA)))
        {
            Hit();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_GAME_AREA))
        {
            this.gameObject.SetActive(false);
        }
    }
}
