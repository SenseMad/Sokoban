using LevelManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
  /// <summary>
  /// ��������� �� ������
  /// </summary>
  public abstract class LevelBehaviour : SingletonInSceneNoInstance<LevelBehaviour>
  {
    #region UNITY

    protected virtual new void Awake()
    {
      base.Awake();
    }

    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void LateUpdate() { }

    #endregion

    //======================================

    /// <summary>
    /// ���������� ������ ������
    /// </summary>
    public abstract LevelData GetCurrentLevelData();

    /// <summary>
    /// ���������� �������� �� ������
    /// </summary>
    public abstract LevelProgressData GetCurrentLevelProgressData();

    //======================================

    public abstract bool IsFoodCollected();

    /// <summary>
    /// �������� ������� � ������
    /// </summary>
    /// <param name="box"></param>
   // public abstract void AddBoxToList(Box box);

    //======================================
  }
}