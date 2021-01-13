using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
  [SerializeField]
  protected float speed = 10f;

  [SerializeField]
  protected float shotDelay = 1f;

  [SerializeField]
  protected float shotStartOffset = 0f;

  [SerializeField]
  protected GameObject bullet = default;

  virtual protected void Start()
  {
    StartCoroutine(Shot());
  }

  virtual protected IEnumerator Shot()
  {
    yield break;
  }
}
