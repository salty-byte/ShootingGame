using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : SingletonMonoBehaviour<MoveManager>
{
  List<MoveSequence> sequences = new List<MoveSequence>();

  void Update()
  {
    sequences.FindAll(v => v.Status == MoveStatus.WAITING)
      .ForEach(v => StartCoroutine(Move(v)));

    sequences.RemoveAll(v => v.Status == MoveStatus.FINISHED);
  }

  public MoveSequence CreateSequence(Transform target)
  {
    var sequence = new MoveSequence(target);
    sequences.Add(sequence);
    return sequence;
  }

  public void AppendSequence(MoveSequence sequence)
  {
    if (sequence == null)
    {
      return;
    }
    sequences.Add(sequence);
  }

  public IEnumerator Move(MoveSequence sequence)
  {
    sequence.Status = MoveStatus.MOVING;
    while (sequence.Queue.Count != 0)
    {
      var action = sequence.Queue.Dequeue();
      yield return StartCoroutine(action.DoAction());
    }
    sequence.Status = MoveStatus.FINISHED;
  }
}
