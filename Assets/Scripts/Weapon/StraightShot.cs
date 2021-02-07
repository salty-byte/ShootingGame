using System.Collections;
using UnityEngine;

public class StraightShot : WeaponBase
{
  override protected void Start()
  {
    StartCoroutine(Shot());
  }

  override protected IEnumerator Shot()
  {
    yield return new WaitForSeconds(shotStartOffset);

    while (transform != null)
    {
      var obj = Instantiate(bullet, transform.parent);
      obj.GetComponent<Rigidbody2D>().velocity = transform.forward * speed;

      yield return new WaitForSeconds(shotDelay);
    }
  }
}
