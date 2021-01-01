using System.Collections;
using UnityEngine;

public class Player : Spaceship
{
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
    Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
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