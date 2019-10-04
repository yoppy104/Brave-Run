using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float JAMP_POWER = 6f;

    private const int MAX_COUNT_OF_JAMP = 2;

    private bool is_jamp = false;

    private int count_jamp = 0;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.JampInput())
        {
            is_jamp = true;

        }
    }

    private void FixedUpdate()
    {

        Debug.Log("jamp");
        if (is_jamp)
        {

            Debug.Log("jamp ok");
            if (count_jamp < MAX_COUNT_OF_JAMP)
            {
                rb.velocity = new Vector2(0, JAMP_POWER);
                count_jamp++;

                Debug.Log("jamp complete");
            }
            is_jamp = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstNumbers.TAG_NAME_STAGE))
        {
            if (count_jamp != 0)
            {
                count_jamp = 0;
            }
        }
    }
}
