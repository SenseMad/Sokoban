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
      { Location.Summer, 0 }
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
    /// �������� ���������� ���������� ������� �� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    public int GetNumberLevelsCompleted(Location parLocation)
    {
      if (!tableNumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"������� {parLocation} �� �������!");
        return 0;
      }

      return tableNumberCompletedLevelsLocation[parLocation];
    }

    /// <summary>
    /// ������� ��������� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    public bool OpenNextLevel(Location parLocation)
    {
      if (!tableNumberCompletedLevelsLocation.ContainsKey(parLocation))
      {
        Debug.Log($"������� {parLocation} �� �������!");
        return false;
      }

      tableNumberCompletedLevelsLocation[parLocation]++;
      return true;
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