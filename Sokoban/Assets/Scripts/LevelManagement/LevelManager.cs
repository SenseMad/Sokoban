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

    //======================================

    /// <summary>
    /// True, если уровень завершен
    /// </summary>
    public bool LevelCompleted { get; set; }

    /// <summary>
    /// True, если пауза
    /// </summary>
    public bool IsPause { get; private set; }
    
    /// <summary>
    /// Сетка уровня
    /// </summary>
    public GridLevel GridLevel => _gridLevel;

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
    public UnityEvent<bool> OnPauseEvent { get; } = new UnityEvent<bool>();
    
    /// <summary>
    /// Событие: Уровень завершен
    /// </summary>
    public UnityEvent IsLevelCompleted { get; } = new UnityEvent();
    /// <summary>
    /// Событие: Следующий уровень
    /// </summary>
    public UnityEvent IsNextLevel { get; } = new UnityEvent();
    /// <summary>
    /// Событие: Данные следующего уровня
    /// </summary>
    public UnityEvent<LevelData> IsNextLevelData { get; } = new UnityEvent<LevelData>();
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
      //_currentLevelData = Levels.CurrentSelectedLevelData;

      //ReloadLevel();
    }

    private void LateUpdate()
    {
      if (_currentLevelData == null)
        return;

      if (IsPause)
        return;

      if (LevelCompleted)
        return;
      
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

    /// <summary>
    /// Установить паузу
    /// </summary>
    public void SetPause(bool parValue)
    {
      IsPause = parValue;
      OnPauseEvent?.Invoke(parValue);
    }

    //======================================

    /// <summary>
    /// Обновить текст времени
    /// </summary>
    public string UpdateTextTimeLevel()
    {
      int hours = Mathf.FloorToInt(TimeOnLevel / 3600f);
      int minutes = Mathf.FloorToInt((TimeOnLevel % 3600f) / 60f);
      int seconds = Mathf.FloorToInt(TimeOnLevel % 60f);

      if (hours > 0)
        return $"{hours:00}:{minutes:00}:{seconds:00}";
      else if (minutes > 0)
        return $"{minutes:00}:{seconds:00}";
      else
        return $"{seconds:00}";
    }

    /// <summary>
    /// Загрузить новый уровень
    /// </summary>
    public void UploadNewLevel()
    {
      IsNextLevel?.Invoke();

      var levelData = GetNextLevelData();
      if (levelData != null)
        _currentLevelData = levelData;

      IsNextLevelData?.Invoke(_currentLevelData);

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
      return Levels.GetNextLevelData(_currentLevelData.Location, _currentLevelData.LevelNumber);
    }

    /// <summary>
    /// Открыть следующий уровень
    /// </summary>
    private bool OpenNextLevel()
    {
      gameManager.ProgressData.SaveProgressLevelData(_currentLevelProgressData, _currentLevelData.Location, _currentLevelData.LevelNumber);

      gameManager.ProgressData.OpenNextLevel(_currentLevelData.Location, _currentLevelData.LevelNumber);

      return true;
    }

    //======================================
  }
}