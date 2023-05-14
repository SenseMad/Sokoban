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
    /// Данные о прогрессе игрока
    /// </summary>
    public ProgressData ProgressData { get; set; } = new ProgressData();

    /// <summary>
    /// Данные о настройках
    /// </summary>
    public SettingsData SettingsData { get; set; } = new SettingsData();

    //======================================

    private void Start()
    {
      Levels.GetFullNumberLevelsLocation();
    }

    //======================================



    //======================================



    //======================================
  }
}