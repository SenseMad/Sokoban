using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sokoban.LevelManagement;

namespace Sokoban.GameManagement
{
  public sealed class GameManager : SingletonInGame<GameManager>
  {
    /// <summary>
    /// ������ � ��������� ������
    /// </summary>
    public ProgressData ProgressData { get; set; } = new ProgressData();

    /// <summary>
    /// ������ � ����������
    /// </summary>
    public SettingsData SettingsData { get; set; } = new SettingsData();

    //======================================

    private new void Awake()
    {
      base.Awake();

      Init();

      SettingsData.CurrentLanguage = Language.Russian;
    }

    private void Start()
    {
      Levels.GetFullNumberLevelsLocation();
    }

    //======================================

    private void Init()
    {
      
    }

    //======================================



    //======================================
  }
}