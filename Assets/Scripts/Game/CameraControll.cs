using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{

    private float timecount;
    private Vector3 velo;

    private Transform cameratransform;

    // Start is called before the first frame update
    void Start()
    {
        cameratransform = this.GetComponent<Transform>();
        cameratransform.position = new Vector3(-7.46f, 45.8f,-10);
        velo = new Vector3(0, -1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (velo.y < 0)
        {
            cameratransform.Translate(velo);
            velo.y += 0.025f;
        }
        else if (velo.y >= 0)
        {
            cameratransform.position = new Vector3(-7.46f, 0,-10);
        }
    }
}
