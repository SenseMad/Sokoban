using UnityEngine;

using Sokoban.LevelManagement;
using Sokoban.Save;

namespace Sokoban.GameManagement
{
  public sealed class GameManager : SingletonInGame<GameManager>
  {
    public ProgressData ProgressData { get; set; } = new ProgressData();

    public SettingsData SettingsData { get; set; } = new SettingsData();

    //======================================

    protected override void Awake()
    {
      base.Awake();

      Init();
    }

    private void Start()
    {
      Levels.GetFullNumberLevelsLocation();

#if UNITY_PS4
      Screen.fullScreen = true;
#else
      SettingsData.CreateResolutions();

      Screen.fullScreen = SettingsData.FullScreenValue;
      Screen.SetResolution(SettingsData.Resolutions[SettingsData.CurrentSelectedResolution].width,
        SettingsData.Resolutions[SettingsData.CurrentSelectedResolution].height, SettingsData.FullScreenValue, SettingsData.Resolutions[SettingsData.CurrentSelectedResolution].refreshRate);
#endif
    }

    private void OnApplicationQuit()
    {
#if !UNITY_PS4
      SaveData();
#endif
    }

    //======================================

    private void Init()
    {
      SettingsData.CurrentLanguage = Language.Russian;

      LoadData();
    }

    //======================================

    public void SaveData()
    {
      SaveLoadManager.Instance.SaveData();
    }

    public void ResetAndSaveFile()
    {
      SaveLoadManager.Instance.ResetAndSaveFile();
    }

    private void LoadData()
    {
      SaveLoadManager.Instance.LoadData();
    }

    //======================================
  }
}