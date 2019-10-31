using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private static ScoreManager instance = null;

    public static ScoreManager Instance{ get { return instance; } }

    private int score;

    private int score_max;

    public int Score { get { return score; } set { score = value; } }

    private PlayerUI ui = null;

    public void addScore(int value)
    {
        score += value;

        if (ui == null)
        {
            ui = GameObject.Find(ConstNumbers.GAMEOBJECT_NAME_PLAYER).GetComponent<PlayerUI>();
        }
        ui.SetScore(score);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
