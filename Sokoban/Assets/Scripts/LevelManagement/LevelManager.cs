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
    [Header("������ ������")]
    [SerializeField, Tooltip("������ � ������� ������")]
    private LevelData _currentLevelData;
    [SerializeField, Tooltip("������ � ��������� �� ������� ������")]
    private LevelProgressData _currentLevelProgressData;

    [Header("����� ������")]
    [SerializeField, Tooltip("����� ������")]
    private GridLevel _gridLevel;

    //--------------------------------------

    private GameManager gameManager;

    //private PauseManager pauseManager;

    //======================================

    /// <summary>
    /// True, ���� ������� ��������
    /// </summary>
    public bool LevelCompleted { get; set; }

    /// <summary>
    /// ����� ������
    /// </summary>
    public GridLevel GridLevel => _gridLevel;

    /// <summary>
    /// True, ���� ���� �����������
    /// </summary>
    //public bool IsPause { get => pauseManager.IsPause; }

    //======================================

    /// <summary>
    /// �������: ��������� ��������� "����� ����������� �� ������"
    /// </summary>
    public UnityEvent<float> ChangeTimeOnLevel { get; } = new UnityEvent<float>();

    /// <summary>
    /// �������: ��������� ��������� "���������� �����"
    /// </summary>
    public UnityEvent<int> ChangeNumberMoves { get; } = new UnityEvent<int>();

    /// <summary>
    /// �������: �����
    /// </summary>
    public UnityEvent<bool> IsPause { get; } = new UnityEvent<bool>();

    /// <summary>
    /// �������: ������� ��������
    /// </summary>
    public UnityEvent IsLevelCompleted { get; } = new UnityEvent();
    /// <summary>
    /// �������: ��������� �������
    /// </summary>
    public UnityEvent IsNextLevel { get; } = new UnityEvent();
    /// <summary>
    /// �������: ������������ ������
    /// </summary>
    public UnityEvent IsReloadLevel { get; } = new UnityEvent();

    //======================================

    /// <summary>
    /// �������� ������ � ������� ������
    /// </summary>
    public LevelData GetCurrentLevelData() => _currentLevelData;

    /// <summary>
    /// �������� ������ � ��������� �� ������� ������
    /// </summary>
    public LevelProgressData GetCurrentLevelProgressData() => _currentLevelProgressData;

    //======================================

    /// <summary>
    /// ����� ����������� �� ������
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
    /// ���������� �����
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
    /// �������� �� �������
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
    /// True, ���� ��� ��� �������
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
    /// ��������� ����� �������
    /// </summary>
    public void UploadNewLevel()
    {
      IsNextLevel?.Invoke();

      _currentLevelData = GetNextLevelData();

      ReloadLevel();
    }

    /// <summary>
    /// ������������� �������
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
    /// ����� � ����
    /// </summary>
    public void ExitMenu()
    {
      SceneManager.LoadScene($"MenuScene");
    }

    /// <summary>
    /// �������� ������ ���������� ������
    /// </summary>
    private LevelData GetNextLevelData()
    {
      if (_currentLevelData.LevelNumber < Levels.GetNumberLevelsLocation(_currentLevelData.Location))
      {
        // ������� ������� ������� � ����� �������
        return Levels.GetLevelData(_currentLevelData.Location, _currentLevelData.LevelNumber + 1);
      }

      if ((int)_currentLevelData.Location + 1 <= GetLocation.GetNamesAllLocation().Length - 1)
      {
        // ������� ����� ������� � ������ �������
        return Levels.GetLevelData(_currentLevelData.Location + 1, 1);
      }

      // ������� ������� ������� � ������� �������
      return Levels.GetLevelData(_currentLevelData.Location, _currentLevelData.LevelNumber);
    }

    /// <summary>
    /// ������� ��������� �������
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
    /// ������� ��������� �������
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