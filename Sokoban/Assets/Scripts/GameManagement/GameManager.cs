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

    private void LoadData()
    {
      SaveLoadManager.Instance.LoadData();
    }

    //======================================
  }
}