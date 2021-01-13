using System.Collections;
using UnityEngine;

public class HomingStraightShot : WeaponBase
{
  Transform target;
  public Transform Target
  {
    get => target;
    set => target = value;
  }

  override protected void Start()
  {
    target = Manager.Instance.p1;
    StartCoroutine(Shot());
  }

  override protected IEnumerator Shot()
  {
    yield return new WaitForSeconds(shotStartOffset);

    while (target != null && transform != null)
    {
      var obj = Instantiate(
        bullet,
        transform.position,
        transform.rotation,
        transform.parent
      );
      var direction = target.position - transform.position;
      obj.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;

      yield return new WaitForSeconds(shotDelay);
    }
  }
}
