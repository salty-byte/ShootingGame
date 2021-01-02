using System.Collections;
using UnityEngine;

public class Player : Spaceship
{
  [SerializeField]
  RectTransform fieldRectTransform = default;

  void Start()
  {
    Initialize();
    StartCoroutine(Shot());
  }

  void Update()
  {
    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");
    Vector2 direction = new Vector2(x, y).normalized;
    Move(direction);
  }

  override protected void Initialize()
  {
    base.Initialize();
    fieldRectTransform = GameObject.Find("GameField").GetComponent<RectTransform>();
  }

  IEnumerator Shot()
  {
    while (true)
    {
      var obj = Instantiate(bullet, transform.position, transform.rotation);
      obj.transform.SetParent(transform.parent);
      obj.GetComponent<Bullet>().SetVelocity(transform.up.normalized);

      AudioManager.Instance.PlaySE("shoot");
      yield return new WaitForSeconds(shotDelay);
    }
  }

  /// <summary>
  /// 機体の移動
  /// </summary>
  void Move(Vector2 direction)
  {
    var corners = new Vector3[4];
    fieldRectTransform.GetWorldCorners(corners);
    var min = corners[0];
    var max = corners[2];
    Vector2 pos = transform.position;
    pos += direction * speed * Time.deltaTime;
    pos.x = Mathf.Clamp(pos.x, min.x, max.x);
    pos.y = Mathf.Clamp(pos.y, min.y, max.y);
    transform.position = pos;
  }

  /// <summary>
  /// エネミーやエネミーの攻撃を受けた時の処理
  /// </summary>
  void OnTriggerEnter2D(Collider2D c)
  {
    string layerName = LayerMask.LayerToName(c.gameObject.layer);
    if (layerName == "Bullet (Enemy)")
    {
      Destroy(c.gameObject);
    }

    if (layerName == "Bullet (Enemy)" || layerName == "Enemy")
    {
      Manager.Instance.GameOver();
      Explosion();
      Destroy(gameObject);
    }
  }
}