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
    [SerializeField] private LevelData _menuLevelData;
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

    //======================================

    public bool IsLevelStarted { get; set; }

    public bool LevelCompleted { get; set; }

    public bool IsPause { get; set; }

    public bool IsLevelMenu { get; private set; }
    
    public GridLevel GridLevel => _gridLevel;

    public CinemachineVirtualCamera CinemachineVirtual
    {
      get => cinemachineVirtual;
      set => cinemachineVirtual = value;
    }

    //======================================

    public UnityEvent<float> ChangeTimeOnLevel { get; } = new UnityEvent<float>();

    public UnityEvent<int> ChangeNumberMoves { get; } = new UnityEvent<int>();

    public UnityEvent<bool> OnPauseEvent { get; } = new UnityEvent<bool>();

    public UnityEvent IsLevelCompleted { get; } = new UnityEvent();

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

    private bool IsLevelComplete()
    {
      if (LevelCompleted)
        return true;

      LevelCompleted = true;
      IsLevelCompleted?.Invoke();

      OpenNextLevel();      

      return true;
    }

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

    public void SetPause(bool parValue)
    {
      IsPause = parValue;
      OnPauseEvent?.Invoke(parValue);
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
    }

    public void MenuLevel()
    {
      _currentLevelData = _menuLevelData;
      _gridLevel.CreatingLevelGrid();
      LevelCompleted = true;
      IsLevelMenu = true;
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

      //Destroy(targetObject, 5f);
    }

    private LevelData GetNextLevelData()
    {
      return Levels.GetNextLevelData(_currentLevelData.Location, _currentLevelData.LevelNumber);
    }

    private bool OpenNextLevel()
    {
      gameManager.ProgressData.SaveProgressLevelData(_currentLevelProgressData, _currentLevelData.Location, _currentLevelData.LevelNumber);

      gameManager.ProgressData.OpenNextLevel(_currentLevelData.Location, _currentLevelData.LevelNumber);

      gameManager.SaveData();

      return true;
    }

    //======================================
  }
}