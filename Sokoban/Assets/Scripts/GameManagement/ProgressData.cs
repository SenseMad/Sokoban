using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sokoban.LevelManagement;

namespace Sokoban.GameManagement
{
  /// <summary>
  /// ������ � ��������� ������
  /// </summary>
  [System.Serializable]
  public sealed class ProgressData
  {
    /// <summary>
    /// ������� ���������� ���������� ������� �� �������
    /// </summary>
    public Dictionary<Location, int> tableNumberCompletedLevelsLocation = new Dictionary<Location, int>()
    {
      { Location.Summer, 2 },
      { Location.Winter, 1 }
    };

    /// <summary>
    /// �������� �� �������
    /// </summary>
    public Dictionary<Location, Dictionary<int, LevelProgressData>> levelProgressData = new Dictionary<Location, Dictionary<int, LevelProgressData>>();

    //======================================

    /// <summary>
    /// ������� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    public bool OpenLocation(Location parLocation)
    {
      if (tableNumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"������� {parLocation} ��� �������!");
        return false;
      }

      tableNumberCompletedLevelsLocation[parLocation] = 0;
      return true;
    }


    /// <summary>
    /// ������� ��������� �������
    /// </summary>
    /// <param name="parCurrentLocation">������� �������</param>
    /// <param name="parCurrentLevel">������� ������ �������</param>
    /// <returns></returns>
    public bool OpenNextLocation(Location parCurrentLocation, int parCurrentLevel)
    {
      if (parCurrentLevel >= Levels.GetNumberLevelsLocation(parCurrentLocation))
      {
        if ((int)parCurrentLocation + 1 <= GetLocation.GetNamesAllLocation().Length - 1)
        {
          if (OpenLocation(parCurrentLocation + 1))
          {
            Debug.Log($"������� {parCurrentLocation + 1} �������!");
            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    /// ������� ��������� �������
    /// </summary>
    /// <param name="parCurrentLocation">������� �������</param>
    /// <param name="parCurrentLevel">������� �������</param>
    public bool OpenNextLevel(Location parCurrentLocation, int parCurrentLevel)
    {
      if (!tableNumberCompletedLevelsLocation.ContainsKey(parCurrentLocation))
      {
        Debug.Log($"������� {parCurrentLocation} ��� �� �������!");
        return false;
      }

      // ���� ������� ������� ������ ��� ����� ���������� ���������� �������
      if (parCurrentLevel >= tableNumberCompletedLevelsLocation[parCurrentLocation])
      {
        tableNumberCompletedLevelsLocation[parCurrentLocation]++;
        // ���� ���������� ����������� ������� ������ ���������� �������
        if (tableNumberCompletedLevelsLocation[parCurrentLocation] >= Levels.GetNumberLevelsLocation(parCurrentLocation))
        {
          tableNumberCompletedLevelsLocation[parCurrentLocation] = Levels.GetNumberLevelsLocation(parCurrentLocation);
          OpenNextLocation(parCurrentLocation, parCurrentLevel);
          return true;
        }

        Debug.Log($"������� {parCurrentLevel + 1} ������!");
        return true;
      }

      return false;
    }

    /// <summary>
    /// �������� ���������� ���������� ������� �� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    public int GetNumberLevelsCompleted(Location parLocation)
    {
      if (!tableNumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"������� {parLocation} ��� �� �������!");
        return 0;
      }

      return tableNumberCompletedLevelsLocation[parLocation];
    }

    /// <summary>
    /// ���������� ��������� �� ������
    /// </summary>
    /// <param name="parLevelProgressData"></param>
    /// <param name="parLocation">�������</param>
    /// <param name="parLevelNumber">����� ������</param>
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
    /// True, ���� ������� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    public bool IsLocationOpen(Location parLocation)
    {
      return tableNumberCompletedLevelsLocation.ContainsKey(parLocation);
    }

    //======================================
  }
}