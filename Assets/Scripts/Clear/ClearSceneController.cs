using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// クリアシーンを全体管理する
/// </summary>
public class ClearSceneController : MonoBehaviour
{

    // 花火のパーティクルオブジェクト
    [SerializeField] private GameObject fire_work_particle;

    [SerializeField] private Text score_text;

    // 花火のオブジェクト配列
    private GameObject[] fire_work_objects;

    // 花火オブジェクトの個数
    private const int NUM_FIRE_WORK = 3;

    private bool[] is_running;


    // Start is called before the first frame update
    void Start()
    {
        // 動的に花火のオブジェクトを生成してキャッシュする
        fire_work_objects = new GameObject[NUM_FIRE_WORK];
        is_running = new bool[NUM_FIRE_WORK];
        for (int i = 0; i < NUM_FIRE_WORK; i++)
        {
            fire_work_objects[i] = Instantiate(fire_work_particle) as GameObject;
            is_running[i] = false;
        }

        SoundManager.Instance.StartBGM(SoundManager.BGMType.CLEAR);

        int result_score = ScoreManager.Instance.Score;
        result_score += 10000;
        score_text.text = result_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < NUM_FIRE_WORK; i++)
        {
            if (!fire_work_objects[i].activeSelf)
            {
                StartCoroutine(DispFireWork(i));
            }
        }   
    }

    // ランダムな時間待機してから、花火を再表示する。
    private IEnumerator DispFireWork(int i)
    {
        if (is_running[i])
        {
            yield break;
        }
        is_running[i] = true;

        float random = Random.Range(0.1f, 2.0f);
        yield return new WaitForSeconds(random);

        fire_work_objects[i].transform.position = RandomPosition();
        fire_work_objects[i].SetActive(true);

        yield return null;

        is_running[i] = false;
    }


    private Vector3 RandomPosition()
    {
        float x = Random.Range(-6.0f, 6.0f);
        float y = Random.Range(-0.5f, 4.0f);

        return new Vector3(x, y, 0.0f);
    }


    // タイトルに遷移するボタンの挙動
    public void OnClickTitleButton()
    {
        SceneManager.LoadScene(ConstNumbers.SCENE_NAME_TITLE);
    }


    // ゲームに遷移するボタンの挙動
    public void OnClickGameButton()
    {
        SceneManager.LoadScene(ConstNumbers.SCENE_NAME_GAME);
    }

}
