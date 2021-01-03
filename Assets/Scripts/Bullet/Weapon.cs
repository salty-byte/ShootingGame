using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{

  [SerializeField]
  float speed = 10f;

  [SerializeField]
  float shotDelay = 1f;

  [SerializeField]
  float shotStartOffset = 0f;

  [SerializeField]
  GameObject bullet = default;

  Transform target;
  public Transform Target
  {
    get => target;
    set => target = value;
  }

  void Start()
  {
    target = Manager.Instance.p1;
    StartCoroutine(Shot());
  }

  public IEnumerator Shot()
  {
    yield return new WaitForSeconds(shotStartOffset);

    while (target != null && transform != null)
    {
      var obj = Instantiate(bullet, transform);
      obj.transform.SetParent(transform.parent);
      var direction = target.position - transform.position;
      obj.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;

      yield return new WaitForSeconds(shotDelay);
    }
  }
}
