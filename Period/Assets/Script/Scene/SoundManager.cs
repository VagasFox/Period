using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BGM_Enum
{
    TITLE = 0,          //タイトル
    PLAY_1,             //ゲームプレイ中１
    PLAY_2,             //ゲームプレイ中２
    ENDING,             //エンディング
    None,				//何も再生していない
}

public enum SE_Enum
{
    SHOT_1 = 0,         //弾発射音(ビーム系)
    SHOT_2,             //弾発射音(弾系)
    NEEDLE,
    WIND,
    ROCK,
}
public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class BGMs
    {
        public BGM_Enum bgmEum;
        public AudioClip bgm;
        public float volume = 1.0f;
    }
    [System.Serializable]
    public class SEs
    {
        public SE_Enum seEnum;
        public AudioClip se;
        public float volume = 1.0f;
    }

    [SerializeField]
    BGMs[] bgmParameter;    //BGMsのステータスリスト
    [SerializeField]
    SEs[] seParameter;  //SEsのステータスリスト

    public AnimationCurve feedInSetting;    //フェードイン用AnimationCurve(設定用)
    public AnimationCurve feedOutSetting;   //フェードアウト用AnimationCurve(設定用)

    //staticで使用するための変数
    static Dictionary<BGM_Enum, BGMs> bgmDictionary;
    static Dictionary<SE_Enum, SEs> seDictionary;

    static AnimationCurve feed_inCurve;     //フェードイン用AnimationCurve
    static AnimationCurve feed_outCurve;    //フェードアウト用AnimationCurve

    static AnimationCurve updateCurve;
    static GameObject myObj;                //このスクリプトがアタッチされているオブジェクト
    static BGM_Enum nowBgmEnum;             //現在再生しているBGMの名前
    static float nowVolume;                 //現在のボリューム

    float curveRate;
    static float beginVolume;               //フェード開始時の音量
    static float endVolume;                 //フェード完了時の音量
    public float frame;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("SoundManager").Length >= 2)
        {
            Destroy(gameObject);
            return;
        }
        curveRate = 0.0f;
        nowVolume = 1.0f;
        updateCurve = null;
        DontDestroyOnLoad(gameObject);
        feed_inCurve = feedInSetting;
        feed_outCurve = feedOutSetting;

        //DictionaryにBGM, SEを追加
        bgmDictionary = new Dictionary<BGM_Enum, BGMs>();
        for (int i = 0; i < bgmParameter.Length; i++)
        {
            bgmDictionary[bgmParameter[i].bgmEum] = bgmParameter[i];
        }
        nowBgmEnum = BGM_Enum.None;
        bgmDictionary[nowBgmEnum] = bgmParameter[0];

        seDictionary = new Dictionary<SE_Enum, SEs>();
        for (int i = 0; i < seParameter.Length; i++)
        {
            seDictionary[seParameter[i].seEnum] = seParameter[i];
        }
        gameObject.AddComponent<AudioSource>();
        myObj = gameObject;
        SetVolume(1.0f);
    }

    void Update()
    {
        //フェードイン処理
        if (updateCurve != null)
        {
            curveRate += 1 / frame;
            SetVolume(Mathf.Lerp(beginVolume, endVolume, updateCurve.Evaluate(curveRate)));
            if (updateCurve.Evaluate(curveRate) >= 1.0f)
            {
                SetVolume(endVolume);
                updateCurve = null;
            }
        }
        else
        {
            curveRate = 0.0f;
        }
    }

    //音声をフェードインさせる
    public static void Fade_inPlay(BGM_Enum bgmEnum, float frame)
    {
        nowBgmEnum = bgmEnum;
        BGMs bgms = bgmDictionary[bgmEnum];
        myObj.GetComponent<AudioSource>().clip = bgms.bgm;
        myObj.GetComponent<AudioSource>().Play();
        updateCurve = feed_inCurve;
        beginVolume = 0.0f;
        endVolume = nowVolume * bgmDictionary[nowBgmEnum].volume;
        myObj.GetComponent<SoundManager>().frame = frame;
    }

    //音声をフェードアウトさせる
    public static void Fade_out(float frame)
    {
        updateCurve = feed_outCurve;
        beginVolume = GetVolume();
        endVolume = 0.0f;
        myObj.GetComponent<SoundManager>().frame = frame;
    }

    //ボリュームを設定する
    public static void SetVolume(float v)
    {
        nowVolume = v;
        myObj.GetComponent<AudioSource>().volume = nowVolume * bgmDictionary[nowBgmEnum].volume;
    }

    //現在のボリュームを取得する
    public static float GetVolume()
    {
        return nowVolume;
    }

    //BGMを再生する
    public static void PlayBGM(BGM_Enum bgmEnum)
    {
        //再生中だったら
        if (bgmEnum == nowBgmEnum) return;
        nowBgmEnum = bgmEnum;
        AudioSource audio = myObj.GetComponent<AudioSource>();
        BGMs bgm = bgmDictionary[bgmEnum];
        audio.Stop();
        audio.clip = bgm.bgm;
        audio.loop = true;
        audio.volume = nowVolume * bgm.volume;
        audio.Play();

    }

    //音声を強制的に止める
    public static void StopBGM()
    {
        myObj.GetComponent<AudioSource>().Stop();
    }

    /// <summary>
    /// 距離に応じてSEのVolumeを変更する
    /// </summary>
    /// <param name="se">se:鳴らすSE</param>
    /// <param name="targetObj">targetObj:メインのObjct</param>
    /// <param name="seObj">seObj:SEを鳴らしているObject</param>
    /// <param name="point">point:距離ごとに減る数値の基準数値</param>
    public static void DistanceOfVolume(SE_Enum se, GameObject targetObj, GameObject seObj, float point)
    {
        PlaySE(se, seObj, true);
        float distance = Vector3.Distance(seObj.transform.position, targetObj.transform.position);

        //指定した距離の範囲に入ったら
		seObj.GetComponent<AudioSource>().volume = 1.0f / distance * point;

    }

    //SEを再生
    public static void PlaySE(SE_Enum seEnum, GameObject obj)
    {
        if (isNowSE(seEnum, obj)) return;
        SEs se = seDictionary[seEnum];
        AudioSource audio;

        audio = isComponent(obj);
        audio.loop = false;
        audio.clip = se.se;
        audio.volume = nowVolume * se.volume;
        audio.Play();
    }

    //SEを再生
    public static void PlaySE(SE_Enum seEnum, GameObject obj, bool loop)
    {
        if (isNowSE(seEnum, obj, loop)) return;
        SEs se = seDictionary[seEnum];
        AudioSource audio;

        audio = isComponent(obj);
        audio.loop = loop;
        audio.clip = se.se;
        audio.volume = nowVolume * se.volume;
        audio.Play();
    }

    //今このSEが再生されていたらtrue
    public static bool isNowSE(SE_Enum seEnum, GameObject obj)
    {
        AudioSource[] audios = obj.GetComponents<AudioSource>();

        if (audios.Length <= 0) return false;

        for (int i = 0; i < audios.Length; i++)
        {
            //再生していなかったら
            if (!audios[i].isPlaying) continue;

            //クリップが同じ場合
            if (audios[i].clip == seDictionary[seEnum].se) return true;
        }
        return false;
    }

    //今このSEが再生されていたらtrue
    public static bool isNowSE(SE_Enum seEnum, GameObject obj, bool loop)
    {
        AudioSource[] audios = obj.GetComponents<AudioSource>();

        if (audios.Length <= 0) return false;

        for (int i = 0; i < audios.Length; i++)
        {
            //再生していなかったら
            if (!audios[i].isPlaying) continue;

            //クリップが同じ場合
            if (audios[i].clip == seDictionary[seEnum].se) return true;
        }
        return false;
    }

    //SEを再生するAudioSourceを取得
    static AudioSource isComponent(GameObject obj)
    {
        AudioSource ans = null;
        //オブジェクトが持っているAudioSourceを全て取得
        AudioSource[] audio = obj.GetComponents<AudioSource>();

        for (int i = 0; i < audio.Length; i++)
        {
            if (!audio[i].isPlaying)//再生していないAudioSourceがあったら
            {
                ans = audio[i];//それを格納
                break;
            }
        }
        //再生していないAudioSourceがない場合
        if (ans == null)
        {
            ans = obj.AddComponent<AudioSource>();  //新しくADDComponentする
        }

        return ans;
    }
}
