using System.Collections;
using UnityEngine;

public class MoveBezierAction : BaseAction
{
  Transform target;

  Vector3 controlPoint;

  Vector3 toPosition;

  float time;

  public Vector3 CalcBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t0)
  {
    float t1 = 1 - t0;
    return t1 * t1 * p0 + 2 * t0 * t1 * p1 + t0 * t0 * p2;
  }

  public MoveBezierAction(Transform target, Vector3 toPosition, Vector3 controlPoint, float time = 3f)
  {
    this.target = target;
    this.time = time;
    this.controlPoint = controlPoint;
    this.toPosition = toPosition;
  }

  override public IEnumerator DoAction()
  {
    if (target == null)
    {
      yield break;
    }

    float totalTime = 0f;
    Vector3 origin = target.position;
    while (totalTime < time && target != null)
    {
      target.position = CalcBezier(origin, controlPoint, toPosition, totalTime / time);
      totalTime += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }

    if (target != null)
    {
      target.position = toPosition;
    }
  }
}
