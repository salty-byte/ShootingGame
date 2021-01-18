using System.Collections;
using UnityEngine;

public class Manager : SingletonMonoBehaviour<Manager>
{
  [SerializeField]
  GameObject player = default;

  GameObject title;

  GameObject playerObjectField;

  GameObject enemyObjectField;

  public Transform p1;

  override protected void Awake()
  {
    if (!base.AwakeInit())
    {
      return;
    }
    InitializeField();
  }

  void Start()
  {
    title = GameObject.Find("MainPanel/Title");
  }

  void Update()
  {
    if (IsPlaying() == false && Input.GetKeyDown(KeyCode.X))
    {
      Initialize();
      GameStart();
    }
  }

  void Initialize()
  {
    InitializeField();
    BackgroundManager.Instance.Initialize();
    Score.Instance.Initialize();
    EnemyEmitter.Instance.Initialize(enemyObjectField);
  }

  void InitializeField()
  {
    if (playerObjectField)
    {
      Destroy(playerObjectField);
    }

    if (enemyObjectField)
    {
      Destroy(enemyObjectField);
    }

    playerObjectField = new GameObject("PlayerObjectField");
    enemyObjectField = new GameObject("EnemyObjectField");
  }

  void GameStart()
  {
    title.SetActive(false);

    var obj = Instantiate(player, playerObjectField.transform);
    p1 = obj.transform;

    StartCoroutine(EmitEnemies());
  }

  IEnumerator EmitEnemies()
  {
    yield return StartCoroutine(EnemyEmitter.Instance.Loop());
    BackgroundManager.Instance.Stop();
    yield return StartCoroutine(WaitUntilEnemiesDisposed());
    GameOver();
  }

  IEnumerator WaitUntilEnemiesDisposed()
  {
    GameObject emitField = EnemyEmitter.Instance.GetEmitField();

    while (IsPlaying())
    {
      if (emitField.transform.childCount == 0)
      {
        yield break;
      }
      yield return new WaitForEndOfFrame();
    }
  }

  public void GameOver()
  {
    Score.Instance.Save();
    title.SetActive(true);
    EnemyEmitter.Instance.IsPlaying = false;
  }

  public bool IsPlaying()
  {
    return title.activeSelf == false;
  }
}