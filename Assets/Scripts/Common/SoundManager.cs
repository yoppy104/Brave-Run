using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音声を統合管理するクラス
/// </summary>
public class SoundManager : MonoBehaviour
{

    private static SoundManager instance;

    public enum BGMType
    {
        FANFALE,
        TITLE,
        GAME,
        CLEAR,
        GAMEOVER
    }

    [SerializeField] private AudioClip main;
    [SerializeField] private AudioClip fanfale;
    [SerializeField] private AudioClip title;
    [SerializeField] private AudioClip clear;
    [SerializeField] private AudioClip gameover;

    private Dictionary<BGMType, AudioClip> bgms;

    [SerializeField] private AudioSource now_play;

    private bool is_fanfale = false;

    public static SoundManager Instance
    {
        get { return instance; }
    }

    // BGMの再生
    public void StartBGM(BGMType type)
    {
        if (type == BGMType.FANFALE)
        {
            now_play.loop = false;
            is_fanfale = true;
        }
        if (now_play != null)
        {
            now_play.Stop();
        }
        now_play.clip = bgms[type];
        now_play.Play();
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

        bgms = new Dictionary<BGMType, AudioClip>();
        bgms[BGMType.CLEAR] = clear;
        bgms[BGMType.FANFALE] = fanfale;
        bgms[BGMType.GAME] = main;
        bgms[BGMType.GAMEOVER] = gameover;
        bgms[BGMType.TITLE] = title;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_fanfale)
        {
            if (!now_play.isPlaying)
            {
                is_fanfale = false;
                now_play.loop = true;
                StartBGM(BGMType.TITLE);
            }
        }
    }
}
