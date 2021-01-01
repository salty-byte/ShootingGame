using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : SingletonMonoBehaviour<BackgroundManager>
{
  [SerializeField]
  List<Background> backgrounds = default;

  float elapsedTime = 0;

  void Update()
  {
    elapsedTime += Time.deltaTime;
    backgrounds.ForEach(v => v.CalcOffset(elapsedTime));
  }

  public void Initialize()
  {
    elapsedTime = 0;
  }
}
