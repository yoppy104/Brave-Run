using System.Collections;
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

    [SerializeField] private AudioSource main;
    [SerializeField] private AudioSource fanfale;
    [SerializeField] private AudioSource title;
    [SerializeField] private AudioSource clear;
    [SerializeField] private AudioSource gameover;

    private Dictionary<BGMType, AudioSource> bgms;

    public SoundManager Instantiate
    {
        get { return instance; }
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

        bgms = new Dictionary<BGMType, AudioSource>();
        bgms[BGMType.CLEAR] = clear;
        bgms[BGMType.FANFALE] = fanfale;
        bgms[BGMType.GAME] = main;
        bgms[BGMType.GAMEOVER] = gameover;
        bgms[BGMType.TITLE] = title;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
