using UnityEngine;

public class Enemy : Spaceship
{
  [SerializeField]
  int hp = 1;
  public int Hp
  {
    get => hp;
    set => hp = value;
  }

  [SerializeField]
  int point = 100;
  public int Point
  {
    get => point;
    set => point = value;
  }

  void Start()
  {
    Initialize();
  }

  /// <summary>
  /// プレイヤーの攻撃を受けた時の処理
  /// </summary>
  void OnTriggerEnter2D(Collider2D c)
  {
    var layerName = LayerMask.LayerToName(c.gameObject.layer);
    if (layerName != "Bullet (Player)")
    {
      return;
    }

    Destroy(c.gameObject);

    var bullet = c.transform.GetComponent<Bullet>();
    hp -= bullet.Power;
    if (hp <= 0)
    {
      Score.Instance.AddPoint(point);
      Explosion();
      Destroy(gameObject);
    }
    else
    {
      animator.SetTrigger("Damage");
    }
  }
}