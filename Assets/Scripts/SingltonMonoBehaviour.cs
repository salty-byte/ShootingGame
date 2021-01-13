using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
  static T instance;

  public static T Instance
  {
    get
    {
      if (instance == null)
      {
        Type t = typeof(T);
        instance = (T)FindObjectOfType(t);

        if (instance == null)
        {
          Debug.LogError(t + " is not found.");
        }
      }
      return instance;
    }
  }

  virtual protected void Awake()
  {
    AwakeInit();
    //DontDestroyOnLoad(this.gameObject);
  }

  protected bool AwakeInit()
  {
    if (this != Instance)
    {
      Destroy(this);
      Debug.LogError(typeof(T) + " already exists.");
      return false;
    }
    return true;
  }
}
