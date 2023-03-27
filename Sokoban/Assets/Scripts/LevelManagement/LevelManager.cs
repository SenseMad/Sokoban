using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using GameManagement;
using Sokoban.GridEditor;
using Sokoban.UI;
using UnityEngine.InputSystem;

namespace LevelManagement
{
  public sealed class LevelManager : MonoBehaviour
  {
    private static LevelManager _instance;

    //======================================

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

    public static LevelManager Instance
    {
      get
      {
        if (_instance == null) { _instance = FindObjectOfType<LevelManager>(); }
        return _instance;
      }
    }

    /// <summary>
    /// True, ���� ������� ��������
    /// </summary>
    public bool LevelCompleted { get; set; }

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

    private void Awake()
    {
      gameManager = GameManager.Instance;

      if (_instance != null && _instance != this)
      {
        Destroy(this);
        return;
      }
      _instance = this;
    }

    private void Start()
    {
      //pauseManager = FindObjectOfType<PauseManager>();

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
    /// �������� ������ ���������� ������
    /// </summary>
    private LevelData GetNextLevelData()
    {
      if (_currentLevelData.LevelNumber < Levels.GetNumberLevelsLocation(_currentLevelData.Location))
      {
        // ������� ������� ������� � ����� �������
        return Levels.GetLevelData(_currentLevelData.Location, _currentLevelData.LevelNumber + 1);
      }

      if ((int)_currentLevelData.Location + 1 <= System.Enum.GetValues(typeof(Location)).Length - 1)
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
      if ((int)_currentLevelData.Location + 1 <= System.Enum.GetValues(typeof(Location)).Length - 1)
      {
        gameManager.ProgressData.OpenLocation(_currentLevelData.Location + 1);
        return true;
      }

      return false;
    }

    //======================================
  }
}