using System.Collections;
using UnityEngine;

public class WaitAction : BaseAction
{
  float time;

  public WaitAction(float time)
  {
    this.time = time;
  }

  override public IEnumerator DoAction()
  {
    yield return new WaitForSeconds(time);
  }
}
