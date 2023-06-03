using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.LevelManagement
{
  /// <summary>
  /// Класс, который хранит уровни
  /// </summary>
  [System.Serializable]
  public sealed class Levels
  {
    /// <summary>
    /// Таблица количества уровней на локации
    /// </summary>
    private readonly static Dictionary<Location, int> levelTable = new Dictionary<Location, int>();

    //======================================

    /// <summary>
    /// Текущие данные выбранного уровня
    /// </summary>
    public static LevelData CurrentSelectedLevelData;

    //======================================

    /// <summary>
    /// Получить путь до хранения уровней
    /// </summary>
    /// <param name="parLocation">Локация</param>
    public static string GetPathToStorageLevels(Location parLocation)
    {
      return $"Locations/{parLocation}";
    }

    /// <summary>
    /// Получить путь до хранения уровней
    /// </summary>
    /// <param name="parLocation">Локация</param>
    /// <param name="parNumLevel">Номер уровня</param>
    public static string GetPathToStorageLevels(Location parLocation, int parNumLevel)
    {
      return $"Locations/{parLocation}/{parLocation}_{parNumLevel}";
    }

    //======================================

    /// <summary>
    /// Получить количество уровней на локации
    /// </summary>
    public static int GetNumberLevelsLocation(Location parLocation)
    {
      if (!levelTable.ContainsKey(parLocation))
      {
        Debug.LogError($"Локация {parLocation} не найдена!");
        return 0;
      }

      return levelTable[parLocation];
    }

    /// <summary>
    /// Получить локацию в таблице
    /// </summary>
    /// <param name="parLocation">Локация</param>
    public static bool GetLocationTable(Location parLocation)
    {
      if (!levelTable.ContainsKey(parLocation))
        return false;

      return true;
    }

    //======================================

    /// <summary>
    /// Получить все количество уровней на каждой локации
    /// </summary>
    public static void GetFullNumberLevelsLocation()
    {
      foreach (var location in GetLocation.GetNamesAllLocation())
      {
        var levelData = Resources.LoadAll<LevelData>(GetPathToStorageLevels(location));

        if (levelData.Length == 0)
          continue;

        if (!levelTable.ContainsKey(location))
          levelTable[location] = 0;

        levelTable[location] = levelData.Length;
      }
    }

    //======================================

    /// <summary>
    /// Получить данные уровеня
    /// </summary>
    /// <param name="parLocation">Локация</param>
    /// <param name="parNumLevel">Номер уровня</param>
    public static LevelData GetLevelData(Location parLocation, int parNumLevel)
    {
      if (!GetLocationTable(parLocation))
        return null;

      var retLevelData = Resources.Load<LevelData>(GetPathToStorageLevels(parLocation, parNumLevel));

      if (retLevelData == null)
      {
        //Debug.LogError($"Локация {parLocation} с номером {parNumLevel} не найдена!");
        return null;
      }

      return retLevelData;
    }

    /// <summary>
    /// Получить данные следующего уровня
    /// </summary>
    /// <param name="parCurrentLocation">Текущая локация</param>
    /// <param name="parCurrentLevel">Текущий уровень</param>
    public static LevelData GetNextLevelData(Location parCurrentLocation, int parCurrentLevel)
    {
      if (parCurrentLevel < GetNumberLevelsLocation(parCurrentLocation))
        return GetLevelData(parCurrentLocation, parCurrentLevel + 1); // Вернуть текущую локацию и новый уровень

      if ((int)parCurrentLocation + 1 <= GetLocation.GetNamesAllLocation().Length - 1)
        return GetLevelData(parCurrentLocation + 1, 1); // Вернуть новую локацию и первый уровень

      return GetLevelData(parCurrentLocation, parCurrentLevel); // Вернуть текущую локацию и текущий уровень
    }

    /// <summary>
    /// Получить список данных об уровнях
    /// </summary>
    /// <param name="parLocation">Локация</param>
    public static List<LevelData> GetListLevelData(Location parLocation)
    {
      var retLevelData = new List<LevelData>();

      LevelData[] listDataLevels = Resources.LoadAll<LevelData>(GetPathToStorageLevels(parLocation));

      for (int i = 0; i < listDataLevels.Length - 1; i++)
      {
        for (int j = 0; j < listDataLevels.Length - i - 1; j++)
        {
          if (listDataLevels[j].LevelNumber > listDataLevels[j + 1].LevelNumber)
          {
            var temp = listDataLevels[j];
            listDataLevels[j] = listDataLevels[j + 1];
            listDataLevels[j + 1] = temp;
          }
        }
      }

      for (int i = 0; i < listDataLevels.Length; i++)
      {
        retLevelData.Add(listDataLevels[i]);
      }

      return retLevelData;
    }

    /// <summary>
    /// Получить список локаций
    /// </summary>
    public static List<Location> GetListLocation()
    {
      var retLocations = new List<Location>();

      foreach (var location in GetLocation.GetNamesAllLocation())
      {
        retLocations.Add(location);
      }

      return retLocations;
    }

    //======================================
  }
}