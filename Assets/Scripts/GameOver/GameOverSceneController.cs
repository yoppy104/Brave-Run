using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSceneController : MonoBehaviour
{

    [SerializeField] private Text score_text;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.StartBGM(SoundManager.BGMType.GAMEOVER);

        float result_score = ScoreManager.Instance.Score;
        score_text.text = result_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        InputManager.KillGame();
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene(ConstNumbers.SCENE_NAME_TITLE);
    }

    public void OnClickGameButton()
    {
        SceneManager.LoadScene(ConstNumbers.SCENE_NAME_GAME);
    }
}
