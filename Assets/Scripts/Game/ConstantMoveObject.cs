using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMoveObject : MonoBehaviour
{

    private Rigidbody2D rb;

    private float speed = 0.000025f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            rb.velocity += Vector2.right * speed;
        }
        else
        {
            transform.position += Vector3.right * speed;
        }
    }
}
