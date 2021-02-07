using System.Collections;
using UnityEngine;

public class MoveSinDeltaAction : BaseAction
{
  Transform target;

  Vector3 positionA;

  Vector3 positionB;

  float time;

  bool isLoop;

  bool isAccel;

  bool useCurrentPosition;

  public MoveSinDeltaAction(Transform target, Vector3 toPosition, float time, bool isAccel = false)
   : this(target, default, toPosition, time, isAccel)
  {
    this.useCurrentPosition = true;
  }

  public MoveSinDeltaAction(Transform target, Vector3 positionA, Vector3 positionB, float time = -1f, bool isAccel = false)
  {
    this.target = target;
    this.positionA = positionA;
    this.positionB = positionB;
    this.isAccel = isAccel;
    this.time = time;
    this.isLoop = time < 0;
    this.useCurrentPosition = false;
  }

  override public IEnumerator DoAction()
  {
    float totalTime = 0f;
    positionA = useCurrentPosition ? target.position : positionA;
    while (target != null && (isLoop || totalTime < time))
    {
      float v = isAccel ? Mathf.PingPong(totalTime, 1) : (Mathf.Sin(totalTime) + 1) / 2;
      target.position = Vector3.Lerp(positionA, positionB, v);
      totalTime += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }
  }
}
