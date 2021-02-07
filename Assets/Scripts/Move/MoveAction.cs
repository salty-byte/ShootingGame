using System.Collections;
using UnityEngine;

public class MoveAction : BaseAction
{
  Transform target;

  Vector3 dist;

  float time;

  public MoveAction(Transform target, Vector3 dist, float time)
  {
    this.time = time;
    this.target = target;
    this.dist = dist;
  }

  override public IEnumerator DoAction()
  {
    if (target == null)
    {
      yield break;
    }

    Vector3 origin = target.position;
    float totalTime = 0f;
    while (totalTime < time && target != null)
    {
      target.position = Vector3.Lerp(origin, dist, totalTime / time);
      totalTime += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }

    if (target != null)
    {
      target.position = dist;
    }
  }
}
