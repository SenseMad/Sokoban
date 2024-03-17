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
        #region Settings

        MusicValue = settingsData.MusicValue,
        SoundValue = settingsData.SoundValue,
        FullScreenValue = settingsData.FullScreenValue,
        VSyncValue = settingsData.VSyncValue,
        CurrentLanguage = settingsData.CurrentLanguage,

        #endregion

        #region ProgressData

        NumberCompletedLevelsLocation = progressData.NumberCompletedLevelsLocation,
        LevelProgressData = progressData.LevelProgressData,

        CurrentActiveIndexSkin = progressData.CurrentActiveIndexSkin,
        LocationLastLevelPlayed = progressData.LocationLastLevelPlayed,
        IndexLastLevelPlayed = progressData.IndexLastLevelPlayed,
        AmountFoodCollected = progressData.AmountFoodCollected,
        PurchasedSkins = progressData.PurchasedSkins,
        TotalNumberMoves = progressData.TotalNumberMoves

        #endregion
      };
    }

    private static void SetGameData(GameData parData)
    {
      GameManager gameManager = GameManager.Instance;

      ProgressData progressData = gameManager.ProgressData;
      SettingsData settingsData = gameManager.SettingsData;

      #region Settings

      settingsData.MusicValue = parData.MusicValue;
      settingsData.SoundValue = parData.SoundValue;
      settingsData.FullScreenValue = parData.FullScreenValue;
      settingsData.VSyncValue = parData.VSyncValue;
      settingsData.CurrentLanguage = parData.CurrentLanguage;

      #endregion

      #region ProgressData

      progressData.NumberCompletedLevelsLocation = parData.NumberCompletedLevelsLocation;
      progressData.LevelProgressData = parData.LevelProgressData ?? new Dictionary<Location, Dictionary<int, LevelManagement.LevelProgressData>>();
      
      progressData.CurrentActiveIndexSkin = parData.CurrentActiveIndexSkin;
      progressData.LocationLastLevelPlayed = parData.LocationLastLevelPlayed;
      progressData.IndexLastLevelPlayed = parData.IndexLastLevelPlayed;
      progressData.AmountFoodCollected = parData.AmountFoodCollected;
      progressData.PurchasedSkins = parData.PurchasedSkins;
      progressData.TotalNumberMoves = parData.TotalNumberMoves;

      #endregion
    }

    //======================================
  }
}