using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class Spaceship : MonoBehaviour
{
  [SerializeField]
  protected float speed;

  [SerializeField]
  protected float shotDelay;

  [SerializeField]
  protected GameObject bullet = default;

  [SerializeField]
  GameObject explosion = default;

  public Animator animator { get; private set; }

  public Rigidbody2D rigid2d { get; private set; }

  /// <summary>
  /// 初期化処理
  /// </summary>
  protected void Initialize()
  {
    animator = GetComponent<Animator>();
    rigid2d = GetComponent<Rigidbody2D>();
  }

  /// <summary>
  /// 爆発エフェクトを発生する
  /// </summary>
  protected void Explosion()
  {
    var obj = Instantiate(explosion, transform.position, transform.rotation);
    obj.transform.SetParent(transform.parent);
  }
}