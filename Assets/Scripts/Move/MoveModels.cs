using UnityEngine;

public class MoveModels
{
  public static MoveSequence Create(Transform target, int t)
  {
    switch (t)
    {
      case 1:
        return Move01(target);
      case 2:
        return Move02(target);
      case 3:
        return Move03(target);
      case 4:
        return Move04(target);
      case 5:
        return Move05(target);
      default:
        return null;
    }
  }

  static MoveSequence Move01(Transform target)
  {
    return new MoveSequence(target)
      .AppendMoveStraight(Vector3.down, speed: 1f);
  }

  static MoveSequence Move02(Transform target)
  {
    float diff = target.position.x > 0 ? -1.0f : 1.0f;
    float midX = target.position.x + diff;
    float midcX = target.position.x + (diff / 2);
    Vector3 mid = new Vector3(midX, 0f);
    Vector3 midControl = new Vector3(midcX, -4f);

    float toX = target.position.x + (diff * 2);
    float tocX = target.position.x + (diff * 3 / 2);
    Vector3 to = new Vector3(toX, -target.position.y);
    Vector3 toControl = new Vector3(tocX, 4f);

    return new MoveSequence(target)
      .AppendMoveBezier(mid, midControl, 3.5f)
      .AppendMoveBezier(to, toControl, 3.5f)
      .AppendMoveStraight(Vector3.down, speed: 1.5f);
  }

  static MoveSequence Move03(Transform target)
  {
    float diff = target.position.x > 0 ? -2.0f : 2.0f;
    float midX = target.position.x + diff;
    float midcX = target.position.x + (diff / 2);
    Vector3 mid = new Vector3(midX, 0f);
    Vector3 midControl = new Vector3(midcX, -3f);

    float toX = target.position.x + (diff * 2);
    float tocX = target.position.x + (diff * 3 / 2);
    Vector3 to = new Vector3(toX, -target.position.y);
    Vector3 toControl = new Vector3(tocX, 3f);

    return new MoveSequence(target)
      .AppendMoveBezier(mid, midControl, 3f)
      .AppendMoveBezier(to, toControl, 3f)
      .AppendMoveStraight(Vector3.down, speed: 1.5f);
  }

  static MoveSequence Move04(Transform target)
  {
    Vector3 to = target.position;
    Vector3 toControl = new Vector3(to.x, -4f);

    return new MoveSequence(target)
      .AppendMoveBezier(to, toControl, 3f)
      .AppendMoveStraight(Vector3.up, speed: 2f);
  }

  static MoveSequence Move05(Transform target)
  {
    Vector3 to = target.position;
    float sign = Mathf.Sign(to.x);
    to.x += 1f * sign;
    Vector3 toControl = new Vector3(-6f * sign, to.y - 3f);

    return new MoveSequence(target)
      .AppendMoveBezier(to, toControl, 5f);
  }
}
