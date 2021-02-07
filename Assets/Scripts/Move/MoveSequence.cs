using System.Collections.Generic;
using UnityEngine;

public class MoveSequence
{
  MoveStatus status;
  public MoveStatus Status
  {
    get => status;
    set => status = value;
  }

  Transform target;
  public Transform Target
  {
    get => target;
    set => target = value;
  }

  BaseAction current;

  Queue<BaseAction> queue;
  public Queue<BaseAction> Queue
  {
    get => queue;
  }

  public MoveSequence() : this(null) { }

  public MoveSequence(Transform target)
  {
    this.target = target;
    this.status = MoveStatus.WAITING;
    queue = new Queue<BaseAction>();
  }

  public MoveSequence AppendAction(BaseAction action)
  {
    queue.Enqueue(action);
    return this;
  }

  public MoveSequence AppendDelay(float time)
  {
    queue.Enqueue(new WaitAction(time));
    return this;
  }

  public MoveSequence AppendMoveTo(Vector3 dist, float time)
  {
    queue.Enqueue(new MoveAction(target, dist, time));
    return this;
  }

  public MoveSequence AppendMoveStraight(Vector3 direction, float speed = 1.0f, float time = -1f)
  {
    queue.Enqueue(new MoveDeltaAction(target, direction, speed, time));
    return this;
  }

  public MoveSequence AppendMoveSin(Vector3 toPosition, float time = -1f, bool isAccel = false)
  {
    queue.Enqueue(new MoveSinDeltaAction(target, toPosition, time, isAccel));
    return this;
  }

  public MoveSequence AppendMoveBezier(Vector3 toPosition, Vector3 controlPoint, float time = 3f)
  {
    queue.Enqueue(new MoveBezierAction(target, toPosition, controlPoint, time));
    return this;
  }
}
