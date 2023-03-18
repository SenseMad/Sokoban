using UnityEngine;

public abstract class SingletonInGame<T> : MonoBehaviour where T : MonoBehaviour
{
  private static T _instance;

  //===========================================

  public static T Instance
  {
    get
    {
      if (_instance == null)
      {
        var singletonObject = new GameObject();
        _instance = singletonObject.AddComponent<T>();
        singletonObject.name = $"{typeof(T)}(Singleton)";

        DontDestroyOnLoad(singletonObject);
      }

      return _instance;
    }
    private set { _instance = value; }
  }

  //===========================================

  protected void Awake()
  {
    if (_instance != null)
    {
      Destroy(this);
    }

    _instance = GetComponent<T>();
  }

  //===========================================
}