using UnityEngine;

public class Manager : SingletonMonoBehaviour<Manager>
{
  [SerializeField]
  GameObject player = default;

  GameObject title;

  GameObject objectField;

  public Transform p1;

  override protected void Awake()
  {
    if (!base.AwakeInit())
    {
      return;
    }
    objectField = new GameObject("ObjectField");
  }

  void Start()
  {
    title = GameObject.Find("Title");
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
    Destroy(objectField);
    objectField = new GameObject("ObjectField");
    BackgroundManager.Instance.Initialize();
    EnemyEmitter.Instance.Initialize(objectField);
  }

  void GameStart()
  {
    title.SetActive(false);

    var obj = Instantiate(player, player.transform.position, player.transform.rotation);
    obj.transform.SetParent(objectField.transform);

    p1 = obj.transform;

    StartCoroutine(EnemyEmitter.Instance.Loop());
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