using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sokoban.GameManagement;
using Sokoban.GridEditor;
using UnityEngine.SceneManagement;

namespace Sokoban.LevelManagement
{
  public sealed class LevelManager : SingletonInSceneNoInstance<LevelManager>
  {
    [Header("ДАННЫЕ УРОВНЯ")]
    [SerializeField, Tooltip("Данные о текущем уровне")]
    private LevelData _currentLevelData;
    [SerializeField, Tooltip("Данные о прогрессе на текущем уровне")]
    private LevelProgressData _currentLevelProgressData;

    [Header("СЕТКА УРОВНЯ")]
    [SerializeField, Tooltip("Сетка уровня")]
    private GridLevel _gridLevel;

    //--------------------------------------

    private GameManager gameManager;

    //private PauseManager pauseManager;

    //======================================

    /// <summary>
    /// True, если уровень завершен
    /// </summary>
    public bool LevelCompleted { get; set; }

    /// <summary>
    /// Сетка уровня
    /// </summary>
    public GridLevel GridLevel => _gridLevel;

    /// <summary>
    /// True, если игра остановлена
    /// </summary>
    //public bool IsPause { get => pauseManager.IsPause; }

    //======================================

    /// <summary>
    /// Событие: Изменение параметра "Время проведенное на уровне"
    /// </summary>
    public UnityEvent<float> ChangeTimeOnLevel { get; } = new UnityEvent<float>();

    /// <summary>
    /// Событие: Изменение параметра "Количество ходов"
    /// </summary>
    public UnityEvent<int> ChangeNumberMoves { get; } = new UnityEvent<int>();

    /// <summary>
    /// Событие: Пауза
    /// </summary>
    public UnityEvent<bool> IsPause { get; } = new UnityEvent<bool>();

    /// <summary>
    /// Событие: Уровень завершен
    /// </summary>
    public UnityEvent IsLevelCompleted { get; } = new UnityEvent();
    /// <summary>
    /// Событие: Следующий уровень
    /// </summary>
    public UnityEvent IsNextLevel { get; } = new UnityEvent();
    /// <summary>
    /// Событие: Перезагрузка уровня
    /// </summary>
    public UnityEvent IsReloadLevel { get; } = new UnityEvent();

    //======================================

    /// <summary>
    /// Получить данные о текущем уровне
    /// </summary>
    public LevelData GetCurrentLevelData() => _currentLevelData;

    /// <summary>
    /// Получить данные о прогрессе на текущем уровне
    /// </summary>
    public LevelProgressData GetCurrentLevelProgressData() => _currentLevelProgressData;

    //======================================

    /// <summary>
    /// Время проведенное на уровне
    /// </summary>
    public float TimeOnLevel
    {
      get => _currentLevelProgressData.TimeOnLevel;
      private set
      {
        _currentLevelProgressData.TimeOnLevel = value;
        ChangeTimeOnLevel?.Invoke(value);
      }
    }

    /// <summary>
    /// Количество ходов
    /// </summary>
    public int NumberMoves
    {
      get => _currentLevelProgressData.NumberMoves;
      set
      {
        _currentLevelProgressData.NumberMoves = value;
        ChangeNumberMoves?.Invoke(value);
      }
    }

    //======================================

    private new void Awake()
    {
      gameManager = GameManager.Instance;
    }

    private void Start()
    {
      _currentLevelData = Levels.CurrentSelectedLevelData;

      ReloadLevel();
    }

    private void LateUpdate()
    {
      if (!LevelCompleted)
        TimeOnLevel += Time.deltaTime;
    }

    //======================================

    /// <summary>
    /// Завершен ли уровень
    /// </summary>
    private bool IsLevelComplete()
    {
      if (LevelCompleted)
        return true;

      LevelCompleted = true;
      IsLevelCompleted?.Invoke();

      OpenNextLevel();

      return true;
    }

    /// <summary>
    /// True, если еда вся собрана
    /// </summary>
    public bool IsFoodCollected()
    {
      var foodObjects = _gridLevel.GetListFoodObjects();
      for (int i = 0; i < foodObjects.Count; i++)
      {
        if (!foodObjects[i].IsFoodCollected)
          return false;
      }

      IsLevelComplete();
      return true;
    }

    //======================================

    /// <summary>
    /// Загрузить новый уровень
    /// </summary>
    public void UploadNewLevel()
    {
      IsNextLevel?.Invoke();

      _currentLevelData = GetNextLevelData();

      ReloadLevel();
    }

    /// <summary>
    /// Перезагрузить уровень
    /// </summary>
    public void ReloadLevel()
    {
      IsReloadLevel?.Invoke();
      _gridLevel.CreatingLevelGrid();

      TimeOnLevel = 0;
      NumberMoves = 0;
      LevelCompleted = false;
    }

    /// <summary>
    /// Выход в меню
    /// </summary>
    public void ExitMenu()
    {
      SceneManager.LoadScene($"MenuScene");
    }

    /// <summary>
    /// Получите данные следующего уровня
    /// </summary>
    private LevelData GetNextLevelData()
    {
      if (_currentLevelData.LevelNumber < Levels.GetNumberLevelsLocation(_currentLevelData.Location))
      {
        // Вернуть текущую локацию и новый уровень
        return Levels.GetLevelData(_currentLevelData.Location, _currentLevelData.LevelNumber + 1);
      }

      if ((int)_currentLevelData.Location + 1 <= GetLocation.GetNamesAllLocation().Length - 1)
      {
        // Вернуть новую локацию и первый уровень
        return Levels.GetLevelData(_currentLevelData.Location + 1, 1);
      }

      // Вернуть текущую локацию и текущий уровень
      return Levels.GetLevelData(_currentLevelData.Location, _currentLevelData.LevelNumber);
    }

    /// <summary>
    /// Открыть следующий уровень
    /// </summary>
    private bool OpenNextLevel()
    {
      gameManager.ProgressData.SaveProgressLevelData(_currentLevelProgressData, _currentLevelData.Location, _currentLevelData.LevelNumber);

      if (_currentLevelData.LevelNumber <= Levels.GetNumberLevelsLocation(_currentLevelData.Location))
      {
        if (_currentLevelData.LevelNumber > gameManager.ProgressData.GetNumberLevelsCompleted(_currentLevelData.Location))
        {
          gameManager.ProgressData.OpenNextLevel(_currentLevelData.Location);

          if (_currentLevelData.LevelNumber >= Levels.GetNumberLevelsLocation(_currentLevelData.Location))
            OpenNextLocation();

          return true;
        }

        return false;
      }

      OpenNextLocation();
      return false;
    }

    /// <summary>
    /// Открыть следующую локацию
    /// </summary>
    public bool OpenNextLocation()
    {
      if ((int)_currentLevelData.Location + 1 <= GetLocation.GetNamesAllLocation().Length - 1)
      {
        gameManager.ProgressData.OpenLocation(_currentLevelData.Location + 1);
        return true;
      }

      return false;
    }

    //======================================
  }
}