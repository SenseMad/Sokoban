using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sokoban.LevelManagement;

namespace Sokoban.GameManagement
{
  /// <summary>
  /// Данные о прогрессе игрока
  /// </summary>
  [System.Serializable]
  public sealed class ProgressData
  {
    /// <summary>
    /// Таблица количества пройденных уровней на локации
    /// </summary>
    public Dictionary<Location, int> tableNumberCompletedLevelsLocation = new Dictionary<Location, int>()
    {
      { Location.Summer, 0 }
    };

    /// <summary>
    /// Прогресс на уровнях
    /// </summary>
    public Dictionary<Location, Dictionary<int, LevelProgressData>> levelProgressData = new Dictionary<Location, Dictionary<int, LevelProgressData>>();

    //======================================

    /// <summary>
    /// Открыть локацию
    /// </summary>
    /// <param name="parLocation">Локация</param>
    public bool OpenLocation(Location parLocation)
    {
      if (tableNumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"Локация {parLocation} уже открыта!");
        return false;
      }

      tableNumberCompletedLevelsLocation[parLocation] = 0;
      return true;
    }

    /// <summary>
    /// Получить количество пройденных уровней на локации
    /// </summary>
    /// <param name="parLocation">Локация</param>
    public int GetNumberLevelsCompleted(Location parLocation)
    {
      if (!tableNumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"Локация {parLocation} не открыта!");
        return 0;
      }

      return tableNumberCompletedLevelsLocation[parLocation];
    }

    /// <summary>
    /// Открыть следующий уровень
    /// </summary>
    /// <param name="parLocation">Локация</param>
    public bool OpenNextLevel(Location parLocation)
    {
      if (!tableNumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"Локация {parLocation} не открыта!");
        return false;
      }

      tableNumberCompletedLevelsLocation[parLocation]++;
      return true;
    }

    /// <summary>
    /// Сохранение прогресса на уровне
    /// </summary>
    /// <param name="parLevelProgressData"></param>
    /// <param name="parLocation">Локация</param>
    /// <param name="parLevelNumber">Номер уровня</param>
    public void SaveProgressLevelData(LevelProgressData parLevelProgressData, Location parLocation, int parLevelNumber)
    {
      if (!levelProgressData.ContainsKey(parLocation))
      {
        levelProgressData[parLocation] = new Dictionary<int, LevelProgressData>();
      }

      levelProgressData[parLocation][parLevelNumber] = parLevelProgressData;
    }

    //======================================

    /// <summary>
    /// True, если локация открыта
    /// </summary>
    /// <param name="parLocation">Локация</param>
    public bool IsLocationOpen(Location parLocation)
    {
      return tableNumberCompletedLevelsLocation.ContainsKey(parLocation);
    }

    //======================================
  }
}