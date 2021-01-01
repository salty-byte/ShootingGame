using System.Collections;
using UnityEngine;

public class MoveDeltaAction : BaseAction
{
  Transform target;

  Vector3 direction;

  float speed;

  float time;

  bool isLoop;

  public MoveDeltaAction(Transform target, Vector3 direction, float speed, float time)
  {
    this.time = time;
    this.target = target;
    this.speed = speed;
    this.direction = direction;
    this.isLoop = time < 0;
  }

  override public IEnumerator DoAction()
  {
    float totalTime = 0f;
    while (target != null && (isLoop || totalTime < time))
    {
      float delta = Time.deltaTime;
      target.position += direction * speed * delta;
      totalTime += delta;
      yield return new WaitForEndOfFrame();
    }
  }
}
