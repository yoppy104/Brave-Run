using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneScript : MonoBehaviour
{
    private bool scenechange;
    private float timecount;
    private Vector2 velo;

    GameObject title;
    GameObject button1;
    GameObject button2;

    RectTransform titletransform;
    RectTransform buttontransform1;
    RectTransform buttontransform2;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.StartBGM(SoundManager.BGMType.FANFALE);
        scenechange = false;
        timecount = 0.5f;
        velo = new Vector2(0, 0);

        title = GameObject.Find("TitleText");
        button1= GameObject.Find("StartButton");
        button2 = GameObject.Find("OptionButton");

        titletransform = title.GetComponent<RectTransform>();
        buttontransform1 = button1.GetComponent<RectTransform>();
        buttontransform2 = button2.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scenechange)
        {
            timecount -= Time.deltaTime;
            titletransform.Translate(velo);
            buttontransform1.Translate(velo);
            buttontransform2.Translate(velo);
            velo.y += 0.025f;
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
