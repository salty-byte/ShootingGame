using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : SingletonMonoBehaviour<BackgroundManager>
{
  [SerializeField]
  List<Background> backgrounds = default;

  [SerializeField]
  bool isMoving = true;

  float elapsedTime = 0;

  void Update()
  {
    if (isMoving)
    {
      elapsedTime += Time.deltaTime;
      backgrounds.ForEach(v => v.CalcOffset(elapsedTime));
    }
  }

  public void Initialize()
  {
    elapsedTime = 0;
    isMoving = true;
  }

  public void Stop()
  {
    isMoving = false;
  }
}
