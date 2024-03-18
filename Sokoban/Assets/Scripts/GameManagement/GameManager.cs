using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

using Sokoban.LevelManagement;
using Sokoban.Save;

namespace Sokoban.GameManagement
{
  public sealed class GameManager : SingletonInGame<GameManager>
  {
    private TransitionBetweenScenes transitionBetweenScenes;

    //======================================

    public ProgressData ProgressData { get; set; } = new ProgressData();

    public SettingsData SettingsData { get; set; } = new SettingsData();

    //======================================

    protected override void Awake()
    {
      base.Awake();

      transitionBetweenScenes = FindAnyObjectByType<TransitionBetweenScenes>();

      StartCoroutine(Init());
    }

    private void Start()
    {
      Levels.GetFullNumberLevelsLocation();

#if UNITY_PS4
      Screen.fullScreen = true;
#else
      SettingsData.CreateResolutions();

      SettingsData.ApplyResolution();
#endif
    }

    private void OnApplicationQuit()
    {
#if !UNITY_PS4
      SaveData();
#endif
    }

    //======================================

    private IEnumerator Init()
    {
      bool initScene = SceneManager.GetActiveScene().name == "InitScene";

      SettingsData.CurrentLanguage = Language.English;

      LoadData();

      yield return new WaitForSeconds(2f);

      if (initScene)
      {
        transitionBetweenScenes.StartSceneChange("GameScene");
      }
    }

    /*private void Init()
    {
      SettingsData.CurrentLanguage = Language.Russian;

      LoadData();
    }*/

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