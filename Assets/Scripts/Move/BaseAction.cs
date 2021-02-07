using System.Collections;

public abstract class BaseAction
{
  virtual public IEnumerator DoAction()
  {
    yield break;
  }
}
