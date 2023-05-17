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
      if (!Levels.GetLocationTable(parLocation))
        return false;

      if (tableNumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"Локация {parLocation} уже открыта!");
        return false;
      }

      tableNumberCompletedLevelsLocation[parLocation] = 0;
      return true;
    }

    /// <summary>
    /// Открыть следующую локацию
    /// </summary>
    /// <param name="parCurrentLocation">Текущая локация</param>
    /// <param name="parCurrentLevel">Текущий уровнь локации</param>
    /// <returns></returns>
    public bool OpenNextLocation(Location parCurrentLocation, int parCurrentLevel)
    {
      if (parCurrentLevel >= Levels.GetNumberLevelsLocation(parCurrentLocation))
      {
        if ((int)parCurrentLocation + 1 <= GetLocation.GetNamesAllLocation().Length - 1)
        {
          if (OpenLocation(parCurrentLocation + 1))
          {
            Debug.Log($"Локация {parCurrentLocation + 1} открыта!");
            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    /// Открыть следующий уровень
    /// </summary>
    /// <param name="parCurrentLocation">Текущая локация</param>
    /// <param name="parCurrentLevel">Текущий уровень</param>
    public bool OpenNextLevel(Location parCurrentLocation, int parCurrentLevel)
    {
      if (!tableNumberCompletedLevelsLocation.ContainsKey(parCurrentLocation))
      {
        Debug.Log($"Локация {parCurrentLocation} еще не открыта!");
        return false;
      }

      int currentNumberLevel = GetNumberLevelsCompleted(parCurrentLocation);
      // Если текущий уровень больше или равен количеству пройденных уровней
      if (parCurrentLevel >= currentNumberLevel)
      {
        currentNumberLevel++;
        // Если количество завершенных уровней больше количества уровней
        if (currentNumberLevel >= Levels.GetNumberLevelsLocation(parCurrentLocation))
        {
          OpenNextLocation(parCurrentLocation, parCurrentLevel);
          return true;
        }

        tableNumberCompletedLevelsLocation[parCurrentLocation] = currentNumberLevel;

        Debug.Log($"Уровень {parCurrentLevel + 1} открыт!");
        return true;
      }

      return false;
    }

    /// <summary>
    /// Получить количество пройденных уровней на локации
    /// </summary>
    /// <param name="parLocation">Локация</param>
    public int GetNumberLevelsCompleted(Location parLocation)
    {
      if (!tableNumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"Локация {parLocation} еще не открыта!");
        return 0;
      }

      return tableNumberCompletedLevelsLocation[parLocation];
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