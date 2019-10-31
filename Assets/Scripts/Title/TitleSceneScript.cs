using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneScript : MonoBehaviour
{
    private bool scenechange;
    private float timecount;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.StartBGM(SoundManager.BGMType.FANFALE);
        scenechange = false;
        timecount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (scenechange)
        {
            timecount -= Time.deltaTime;
            if (timecount < 0)
            {
                SceneManager.LoadScene(ConstNumbers.SCENE_NAME_GAME);
            }
        }
    }

    public void OnClickGameStart()
    {
        Debug.Log("test");
        scenechange = true;
    }
}
