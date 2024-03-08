using System.Collections.Generic;
using UnityEngine;

using Sokoban.LevelManagement;

namespace Sokoban.GameManagement
{
  public sealed class ProgressData
  {
    #region (Tables) Locations/Levels

    public Dictionary<Location, int> NumberCompletedLevelsLocation = new()
    {
      { Location.Summer, 0 }
    };

    public Dictionary<Location, Dictionary<int, LevelProgressData>> LevelProgressData = new();

    #endregion

    public int CurrentActiveIndexSkin { get; set; } = 0;

    public Location LocationLastLevelPlayed { get; set; } = Location.Summer;

    public int IndexLastLevelPlayed { get; set; } = 1;

    //======================================

    public bool OpenLocation(Location parLocation)
    {
      if (!Levels.GetLocationTable(parLocation))
        return false;

      if (NumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"Локация {parLocation} уже открыта!");
        return false;
      }

      NumberCompletedLevelsLocation[parLocation] = 0;
      return true;
    }

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

    public bool OpenNextLevel(Location parCurrentLocation, int parCurrentLevel)
    {
      if (!NumberCompletedLevelsLocation.ContainsKey(parCurrentLocation))
      {
        Debug.Log($"Локация {parCurrentLocation} еще не открыта!");
        return false;
      }

      int currentNumberLevel = GetNumberLevelsCompleted(parCurrentLocation);
      if (parCurrentLevel >= currentNumberLevel)
      {
        currentNumberLevel++;
        if (currentNumberLevel >= Levels.GetNumberLevelsLocation(parCurrentLocation))
        {
          OpenNextLocation(parCurrentLocation, parCurrentLevel);
          return true;
        }

        NumberCompletedLevelsLocation[parCurrentLocation] = currentNumberLevel;

        Debug.Log($"Уровень {parCurrentLevel + 1} открыт!");
        return true;
      }

      return false;
    }

    public int GetNumberLevelsCompleted(Location parLocation)
    {
      if (!NumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"Локация {parLocation} еще не открыта!");
        return 0;
      }

      return NumberCompletedLevelsLocation[parLocation];
    }

    public void SaveProgressLevelData(LevelProgressData parLevelProgressData, Location parLocation, int parLevelNumber)
    {
      if (!LevelProgressData.ContainsKey(parLocation))
      {
        LevelProgressData[parLocation] = new Dictionary<int, LevelProgressData>();
      }

      LevelProgressData[parLocation][parLevelNumber] = parLevelProgressData;
    }

    //======================================

    public bool IsLocationOpen(Location parLocation)
    {
      return NumberCompletedLevelsLocation.ContainsKey(parLocation);
    }

    //======================================
  }
}