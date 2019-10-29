using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private static ScoreManager instance = null;

    public static ScoreManager Instance{ get { return Instance; } }

    private int score;

    private int score_max;

    public int Score { get { return score; } set { score = value; } }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
