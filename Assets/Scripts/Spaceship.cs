using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class Spaceship : MonoBehaviour
{
  [SerializeField]
  GameObject explosion = default;

  public Animator animator { get; private set; }

  public Rigidbody2D rigid2d { get; private set; }

  /// <summary>
  /// 初期化処理
  /// </summary>
  virtual protected void Initialize()
  {
    animator = GetComponent<Animator>();
    rigid2d = GetComponent<Rigidbody2D>();
  }

  /// <summary>
  /// 爆発エフェクトを発生する
  /// </summary>
  protected void Explosion()
  {
    Instantiate(
      explosion,
      transform.position,
      transform.rotation,
      transform.parent
    );
  }
}
