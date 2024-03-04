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
    public int MusicValue = 25;

    public int SoundValue = 25;

    public Language CurrentLanguage = Language.English;

    public int CurrentActiveIndexSkin = 0;

    public Dictionary<Location, int> NumberCompletedLevelsLocation = new();

    public Dictionary<Location, Dictionary<int, LevelProgressData>> LevelProgressData = new();
  }
}