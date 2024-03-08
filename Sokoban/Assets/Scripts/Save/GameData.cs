using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sokoban.LevelManagement;

namespace Sokoban.Save
{
  [Serializable]
  public class GameData
  {
    #region Settings

    public int MusicValue = 25;

    public int SoundValue = 25;

    public Language CurrentLanguage = Language.English;

    #endregion

    public int CurrentActiveIndexSkin = 0;

    #region Level

    public Location LocationLastLevelPlayed = Location.Summer;

    public int IndexLastLevelPlayed = 0;

    public Dictionary<Location, int> NumberCompletedLevelsLocation = new()
    {
      { Location.Summer, 0 }
    };

    public Dictionary<Location, Dictionary<int, LevelProgressData>> LevelProgressData = new();

    #endregion
  }
}