using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// BGMとSEの管理をするマネージャ。
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
  const string BGM_VOLUME_KEY = "bgmvolume";
  const string SE_VOLUME_KEY = "sevolume";
  const float BGM_VOLUME_DEFULT = 0.5f;
  const float SE_VOLUME_DEFULT = 0.4f;

  const string BGM_PATH = "Sounds/BGM";
  const string SE_PATH = "Sounds/SE";

  public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
  public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
  float bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

  string nextBGMName;
  string nextSEName;

  bool isFadeOut = false;

  AudioSource bgmSource;
  List<AudioSource> seSources;
  const int SE_SOURCE_NUM = 10;

  [SerializeField]
  Dictionary<string, AudioClip> bgmDict, seDict;

  override protected void Awake()
  {
    if (!base.AwakeInit())
    {
      return;
    }
    DontDestroyOnLoad(this.gameObject);

    // bgm
    bgmSource = gameObject.AddComponent<AudioSource>();
    bgmSource.playOnAwake = false;
    bgmSource.loop = true;
    bgmSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);

    // se
    seSources = new List<AudioSource>();
    foreach (var _ in Enumerable.Range(0, SE_SOURCE_NUM))
    {
      var s = gameObject.AddComponent<AudioSource>();
      s.playOnAwake = false;
      s.loop = false;
      s.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);
      seSources.Add(s);
    };

    // リソースフォルダから全SE&BGMのファイルを読み込みセット
    bgmDict = new Dictionary<string, AudioClip>();
    seDict = new Dictionary<string, AudioClip>();
    var bgmList = Resources.LoadAll<AudioClip>(BGM_PATH);
    var seList = Resources.LoadAll<AudioClip>(SE_PATH);
    Array.ForEach(bgmList, v => bgmDict[v.name] = v);
    Array.ForEach(seList, v => seDict[v.name] = v);
  }

  /// <summary>
  /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
  /// </summary>
  public void PlaySE(string seName, float delay = 0.0f)
  {
    if (!seDict.ContainsKey(seName))
    {
      Debug.Log(seName + " is not found.");
      return;
    }

    nextSEName = seName;
    Invoke("DelayPlaySE", delay);
  }

  private void DelayPlaySE()
  {
    seSources.Find(v => !v.isPlaying)?.PlayOneShot(seDict[nextSEName]);
  }

  /// <summary>
  /// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
  /// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
  /// </summary>
  public void PlayBGM(string name, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH)
  {
    if (!bgmDict.ContainsKey(name))
    {
      Debug.Log(name + " is not found.");
      return;
    }

    //現在BGMが流れていない時はそのまま流す
    if (!bgmSource.isPlaying)
    {
      nextBGMName = "";
      bgmSource.clip = bgmDict[name];
      bgmSource.Play();
    }
    //違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
    else if (bgmSource.clip.name != name)
    {
      nextBGMName = name;
      FadeOutBGM(fadeSpeedRate);
    }
  }

  /// <summary>
  /// BGMをすぐに止める
  /// </summary>
  public void StopBGM()
  {
    bgmSource.Stop();
  }

  /// <summary>
  /// 現在流れている曲をフェードアウトさせる
  /// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
  /// </summary>
  public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
  {
    bgmFadeSpeedRate = fadeSpeedRate;
    isFadeOut = true;
  }

  void Update()
  {
    if (!isFadeOut)
    {
      return;
    }

    //徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
    bgmSource.volume -= Time.deltaTime * bgmFadeSpeedRate;
    if (bgmSource.volume <= 0)
    {
      bgmSource.Stop();
      bgmSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
      isFadeOut = false;

      if (!string.IsNullOrEmpty(nextBGMName))
      {
        PlayBGM(nextBGMName);
      }
    }

  }

  /// <summary>
  /// BGMとSEのボリュームを別々に変更/保存
  /// </summary>
  public void UpdateVolume(float bgmVolume, float seVolume)
  {
    bgmSource.volume = bgmVolume;
    seSources.ForEach(s => s.volume = seVolume);

    PlayerPrefs.SetFloat(BGM_VOLUME_KEY, bgmVolume);
    PlayerPrefs.SetFloat(SE_VOLUME_KEY, seVolume);
  }
}
