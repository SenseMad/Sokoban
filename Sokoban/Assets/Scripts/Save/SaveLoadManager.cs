using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using Sokoban.GameManagement;

namespace Sokoban.Save
{
  public sealed class SaveLoadManager : SingletonInGame<SaveLoadManager>
  {
    private static string PATH = "";

    //======================================

    protected override void Awake()
    {
      base.Awake();

      PATH = $"{Application.persistentDataPath}/SaveData.dat";

#if UNITY_PS4
      
#endif
    }

    //======================================

    public void SaveData()
    {
      GameData data = GameData();

#if UNITY_PS4
      
#else
      FileStream fileStream = new(PATH, FileMode.Create);
      BinaryFormatter binaryFormatter = new();

      binaryFormatter.Serialize(fileStream, data);
      fileStream.Close();
#endif
    }

    public void LoadData()
    {
#if UNITY_PS4
      
#else
      if (File.Exists(PATH))
      {
        BinaryFormatter binaryFormatter = new();
        FileStream fileStream = new(PATH, FileMode.Open);
        GameData data = (GameData)binaryFormatter.Deserialize(fileStream);
        fileStream.Close();

        SetGameData(data);
      }
#endif
    }

    //======================================

    public void ResetAndSaveFile()
    {
      ResetGameData();
      SaveData();
    }

    public static void ResetGameData()
    {
      SetGameData(new GameData());
    }

    //======================================

    private static GameData GameData()
    {
      GameManager gameManager = GameManager.Instance;

      ProgressData progressData = gameManager.ProgressData;
      SettingsData settingsData = gameManager.SettingsData;

      return new GameData()
      {
        NumberCompletedLevelsLocation = progressData.NumberCompletedLevelsLocation,
        LevelProgressData = progressData.LevelProgressData,
        CurrentActiveIndexSkin = progressData.CurrentActiveIndexSkin,

        MusicValue = settingsData.MusicValue,
        SoundValue = settingsData.SoundValue,
        CurrentLanguage = settingsData.CurrentLanguage
      };
    }

    private static void SetGameData(GameData parData)
    {
      GameManager gameManager = GameManager.Instance;

      ProgressData progressData = gameManager.ProgressData;
      SettingsData settingsData = gameManager.SettingsData;

      progressData.NumberCompletedLevelsLocation = parData.NumberCompletedLevelsLocation ?? new Dictionary<Location, int>();
      progressData.LevelProgressData = parData.LevelProgressData ?? new Dictionary<Location, Dictionary<int, LevelManagement.LevelProgressData>>();
      progressData.CurrentActiveIndexSkin = parData.CurrentActiveIndexSkin;

      settingsData.MusicValue = parData.MusicValue;
      settingsData.SoundValue = parData.SoundValue;
      settingsData.CurrentLanguage = parData.CurrentLanguage;
    }

    //======================================
  }
}