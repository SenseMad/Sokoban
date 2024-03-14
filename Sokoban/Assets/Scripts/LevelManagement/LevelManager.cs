using System;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

using Sokoban.GameManagement;
using Sokoban.GridEditor;
using Sokoban.UI;

namespace Sokoban.LevelManagement
{
  public sealed class LevelManager : SingletonInSceneNoInstance<LevelManager>
  {
    [Header("ДАННЫЕ УРОВНЯ")]
    [SerializeField] private LevelData _currentLevelData;
    [SerializeField] private LevelProgressData _currentLevelProgressData;

    [Header("СЕТКА УРОВНЯ")]
    [SerializeField] private GridLevel _gridLevel;

    [Header("ПАНЕЛИ")]
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _levelCompleteMenu;
    [SerializeField] private Panel _menuPanel;

    //--------------------------------------

    private GameManager gameManager;

    private CinemachineVirtualCamera cinemachineVirtual;

    private bool isCameraRotation;

    private AudioManager audioManager;

    private LevelSounds levelSounds;

    private int tempAmountFoodCollected = 0;

    //======================================

    public bool LevelCompleted { get; set; }

    public bool IsPause { get; set; }

    public bool IsLevelMenu { get; private set; }
    
    public GridLevel GridLevel => _gridLevel;

    public CinemachineVirtualCamera CinemachineVirtual
    {
      get => cinemachineVirtual;
      set => cinemachineVirtual = value;
    }

    public bool IsLevelRunning { get; set; } = false;

    //======================================

    public UnityEvent<float> ChangeTimeOnLevel { get; } = new UnityEvent<float>();

    public UnityEvent<int> ChangeNumberMoves { get; } = new UnityEvent<int>();

    public UnityEvent<bool> OnPauseEvent { get; } = new UnityEvent<bool>();

    public event Action OnLevelCompleted;

    public UnityEvent IsNextLevel { get; } = new UnityEvent();

    public UnityEvent<LevelData> IsNextLevelData { get; } = new UnityEvent<LevelData>();

    public UnityEvent IsReloadLevel { get; } = new UnityEvent();

    public UnityEvent IsMenu { get; } = new UnityEvent();

    //======================================

    public LevelData GetCurrentLevelData() => _currentLevelData;

    public LevelProgressData GetCurrentLevelProgressData() => _currentLevelProgressData;

    //======================================

    public float TimeOnLevel
    {
      get => _currentLevelProgressData.TimeOnLevel;
      private set
      {
        _currentLevelProgressData.TimeOnLevel = value;
        ChangeTimeOnLevel?.Invoke(value);
      }
    }

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

      cinemachineVirtual = FindObjectOfType<CinemachineVirtualCamera>();

      audioManager = AudioManager.Instance;

      levelSounds = GetComponent<LevelSounds>();
    }

    private void Start()
    {
      MenuLevel();
      //_currentLevelData = Levels.CurrentSelectedLevelData;

      //ReloadLevel();
    }

    private void LateUpdate()
    {
      CameraRotationLoop();

      ResetCameraRotation();

      if (_currentLevelData == null)
        return;

      if (IsPause)
        return;

      if (LevelCompleted)
        return;

      if (!_gridLevel.IsLevelCreated)
        return;
      
      TimeOnLevel += Time.deltaTime;
    }

    private void OnEnable()
    {
      OnLevelCompleted += LevelManager_OnLevelCompleted;
    }

    private void OnDisable()
    {
      OnLevelCompleted -= LevelManager_OnLevelCompleted;
    }

    //======================================

    private void CameraRotationLoop()
    {
      if (!IsLevelMenu)
        return;

      float yRotation = cinemachineVirtual.transform.rotation.eulerAngles.y + Time.deltaTime * 5f;
      Quaternion rotation = Quaternion.Euler(48f, yRotation, 0f);
      cinemachineVirtual.transform.rotation = rotation;
    }

    private void ResetCameraRotation()
    {
      if (!isCameraRotation)
        return;

      Quaternion currentRotation = cinemachineVirtual.transform.rotation;
      Quaternion targetQuaternion = Quaternion.Euler(48.0f, 0.0f, 0.0f);
      Quaternion newRotation = Quaternion.Slerp(currentRotation, targetQuaternion, 3f * Time.deltaTime);

      cinemachineVirtual.transform.rotation = newRotation;

      if (Quaternion.Angle(currentRotation, targetQuaternion) < 0.01f)
      {
        isCameraRotation = false;
        cinemachineVirtual.transform.rotation = targetQuaternion;
      }
    }

    private void IsLevelComplete()
    {
      if (LevelCompleted)
        return;

      OnLevelCompleted?.Invoke();
    }

    public bool IsFoodCollected()
    {
      var foodObjects = _gridLevel.GetListFoodObjects();
      tempAmountFoodCollected++;
      for (int i = 0; i < foodObjects.Count; i++)
      {
        if (!foodObjects[i].IsFoodCollected)
          return false;
      }

      IsLevelComplete();
      return true;
    }

    public void SetPause(bool parValue)
    {
      IsPause = parValue;
      OnPauseEvent?.Invoke(parValue);
    }

    //======================================

    private void LevelManager_OnLevelCompleted()
    {
      LevelCompleted = true;

      GameManager.Instance.ProgressData.AmountFoodCollected += tempAmountFoodCollected;
      tempAmountFoodCollected = 0;

      audioManager.OnPlaySound?.Invoke(levelSounds.LevelComplete);

      OpenNextLevel();
    }

    //======================================

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
        return $"{minutes:00}:{seconds:00}";
    }

    /// <summary>
    /// Загрузить новый уровень
    /// </summary>
    public void UploadNewLevel()
    {
      IsNextLevel?.Invoke();

      var levelData = Levels.GetNextLevelData(_currentLevelData.Location, _currentLevelData.LevelNumber);
      if (levelData != null)
        _currentLevelData = levelData;

      IsNextLevelData?.Invoke(_currentLevelData);

      isCameraRotation = true;

      ReloadLevel(_currentLevelData);
    }

    public void ReloadLevel()
    {
      isCameraRotation = true;
      IsReloadLevel?.Invoke();
      _gridLevel.CreatingLevelGrid();

      TimeOnLevel = 0;
      NumberMoves = 0;
      LevelCompleted = false;

      tempAmountFoodCollected = 0;
    }

    public void ReloadLevel(LevelData levelData)
    {
      _currentLevelData = levelData;

      IsNextLevelData?.Invoke(_currentLevelData);

      isCameraRotation = true;

      IsReloadLevel?.Invoke();
      _gridLevel.CreatingLevelGrid();

      TimeOnLevel = 0;
      NumberMoves = 0;
      LevelCompleted = false;
      IsLevelMenu = false;

      _pauseMenu.SetActive(true);
      _levelCompleteMenu.SetActive(true);

      tempAmountFoodCollected = 0;
    }

    public void MenuLevel()
    {
      ProgressData progress = gameManager.ProgressData;

      _currentLevelData = Levels.GetLevelData(progress.LocationLastLevelPlayed, progress.IndexLastLevelPlayed + 1);
      _gridLevel.CreatingLevelGrid();
      LevelCompleted = true;
      IsLevelMenu = true;
    }

    public void SkinReplace()
    {
      _gridLevel.SkinReplace();
    }

    public void ExitMenu()
    {
      //_gridLevel.DeletingLevelObjects();
      _gridLevel.CreatingLevelGrid();
      IsLevelMenu = true;

      _pauseMenu.SetActive(false);
      _levelCompleteMenu.SetActive(false);

      //var targetObject = new GameObject("targetObject");
      //targetObject.transform.position = new Vector3(3, 2, 6f);
      //cinemachineVirtual.Follow = targetObject.transform;
      isCameraRotation = true;

      IsMenu?.Invoke();

      PanelController.Instance.CloseAllPanels();
      PanelController.Instance.SetActivePanel(_menuPanel);

      IsLevelRunning = false;

      //Destroy(targetObject, 5f);
    }

    private void OpenNextLevel()
    {
      gameManager.ProgressData.SaveProgressLevelData(_currentLevelProgressData, _currentLevelData.Location, _currentLevelData.LevelNumber);

      gameManager.ProgressData.OpenNextLevel(_currentLevelData.Location, _currentLevelData.LevelNumber);

      gameManager.SaveData();
    }

    //======================================
  }
}