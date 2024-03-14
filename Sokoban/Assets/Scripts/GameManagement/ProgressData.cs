using System.Collections.Generic;
using UnityEngine;
using System;

using Sokoban.LevelManagement;

namespace Sokoban.GameManagement
{
  public sealed class ProgressData
  {
    private int amountFoodCollected = 0;

    //======================================

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

    public int AmountFoodCollected
    {
      get => amountFoodCollected;
      set
      {
        amountFoodCollected = value;
        OnAmountFoodCollected?.Invoke(value);
      }
    }

    public SortedSet<int> PurchasedSkins { get; set; } = new();

    public int TotalNumberMoves { get; set; } = 0;

    //======================================

    public event Action<int> OnAmountFoodCollected;

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
      if (parCurrentLevel < Levels.GetNumberLevelsLocation(parCurrentLocation))
        return false;

      if ((int)parCurrentLocation + 1 > GetLocation.GetNamesAllLocation().Length - 1)
        return false;

      if (!OpenLocation(parCurrentLocation + 1))
        return false;

      Debug.Log($"Локация {parCurrentLocation + 1} открыта!");
      return true;
    }

    public bool OpenNextLevel(Location parCurrentLocation, int parCurrentLevel)
    {
      if (!NumberCompletedLevelsLocation.ContainsKey(parCurrentLocation))
      {
        Debug.Log($"Локация {parCurrentLocation} еще не открыта!");
        return false;
      }

      int currentNumberLevel = GetNumberLevelsCompleted(parCurrentLocation);
      if (parCurrentLevel <= currentNumberLevel)
        return false;

      currentNumberLevel++;

      if (currentNumberLevel >= Levels.GetNumberLevelsLocation(parCurrentLocation))
      {
        OpenNextLocation(parCurrentLocation, parCurrentLevel);
        NumberCompletedLevelsLocation[parCurrentLocation] = currentNumberLevel;
        return true;
      }

      NumberCompletedLevelsLocation[parCurrentLocation] = currentNumberLevel;

      Debug.Log($"Уровень {parCurrentLevel + 1} открыт!");
      return true;
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