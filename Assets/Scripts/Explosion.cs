using UnityEngine;

public class Explosion : MonoBehaviour
{
  void Start()
  {
    AudioManager.Instance.PlaySE("boom");
  }

  void OnAnimationFinish()
  {
    Destroy(gameObject);
  }
}
