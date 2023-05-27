using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

using Sokoban.LevelManagement;

namespace Sokoban.UI
{
  public class UILevelComplete : MenuUI
  {
    [Header("ПАНЕЛЬ")]
    [SerializeField, Tooltip("Панель пройденного уровн¤")]
    private Panel _levelCompletePanel;

    [Header("ТЕКСТЫ")]
    [SerializeField, Tooltip("Текст времени прохождени¤ уровн¤")]
    private TextMeshProUGUI _textLevelCompletedTime;
    [SerializeField, Tooltip("Текст номера уровня")]
    private TextMeshProUGUI _textLevelNumber;
    [SerializeField, Tooltip("Текст количества ходов")]
    private TextMeshProUGUI _textNumberMoves;

    //--------------------------------------

    //private InputHandler inputHandler;

    private LevelManager levelManager;

    //private PanelController panelController;

    //======================================

    protected override void Awake()
    {
      base.Awake();
      //inputHandler = InputHandler.Instance;

      levelManager = LevelManager.Instance;

      //panelController = PanelController.Instance;
    }

    protected override void OnEnable()
    {
      IsSelected = false;
      indexActiveButton = _listButtons.Count - 1;

      base.OnEnable();

      //IsSelected = false;

      //inputHandler.AI_Player.UI.Select.performed += OnSelect;
      inputHandler.AI_Player.UI.Reload.performed += OnReload;
      //inputHandler.AI_Player.UI.Pause.performed += OnExitMenu;

      levelManager.IsNextLevel.AddListener(panelController.CloseAllPanels);
      levelManager.IsLevelCompleted.AddListener(UpdateText);
    }

    protected override void OnDisable()
    {
      base.OnDisable();

      //inputHandler.AI_Player.UI.Select.performed -= OnSelect;
      inputHandler.AI_Player.UI.Reload.performed -= OnReload;
      //inputHandler.AI_Player.UI.Pause.performed -= OnExitMenu;

      levelManager.IsNextLevel.RemoveListener(panelController.CloseAllPanels);
      levelManager.IsLevelCompleted.RemoveListener(UpdateText);
    }

    protected override void Update()
    {
      if (!levelManager.LevelCompleted)
        return;

      MoveMenuHorizontally();
    }

    //======================================

    /// <summary>
    /// Обновить текст при завершении уровня
    /// </summary>
    private void UpdateText()
    {
      UpdateTextTimeLevel();
      UpdateTextLevelNumber();
      UpdateTextNumberMoves();

      panelController.ShowPanel(_levelCompletePanel);
    }

    /// <summary>
    /// Обновить текст времени на уровне
    /// </summary>
    private void UpdateTextTimeLevel()
    {
      _textLevelCompletedTime.text = $"{levelManager.UpdateTextTimeLevel()}";
    }

    /// <summary>
    /// Обновить текст номера уровня
    /// </summary>
    private void UpdateTextLevelNumber()
    {
      _textLevelNumber.text = $"Level {levelManager.GetCurrentLevelData().LevelNumber}";
    }

    /// <summary>
    /// Обновить текст количества ходов
    /// </summary>
    private void UpdateTextNumberMoves()
    {
      _textNumberMoves.text = $"{levelManager.NumberMoves}";
    }

    protected override void CloseMenu()
    {
      ExitMenu();

      Sound();
    }

    //======================================

    /// <summary>
    /// Следующий уровень
    /// </summary>
    public void NextLevel()
    {
      if (!levelManager.LevelCompleted)
        return;

      levelManager.UploadNewLevel();

      IsSelected = false;
      indexActiveButton = _listButtons.Count - 1;
      IsSelected = true;
    }

    /// <summary>
    /// Перезапуск уровня
    /// </summary>
    public void ReloadLevel()
    {
      if (!levelManager.LevelCompleted)
        return;

      panelController.CloseAllPanels();
      levelManager.ReloadLevel();

      IsSelected = false;
      indexActiveButton = _listButtons.Count - 1;
      IsSelected = true;
    }

    /// <summary>
    /// Выход в меню
    /// </summary>
    public void ExitMenu()
    {
      if (!levelManager.LevelCompleted)
        return;

      levelManager.ExitMenu();

      IsSelected = false;
      indexActiveButton = _listButtons.Count - 1;
      IsSelected = true;
    }

    /// <summary>
    /// Следующий уровень
    /// </summary>
    public void OnSelect(InputAction.CallbackContext context)
    {
      NextLevel();
    }

    /// <summary>
    /// Перезагрузка уровнЯ
    /// </summary>
    public void OnReload(InputAction.CallbackContext context)
    {
      ReloadLevel();
    }

    /// <summary>
    /// Выход в меню
    /// </summary>
    public void OnExitMenu(InputAction.CallbackContext context)
    {
      ExitMenu();
    }

    //======================================
  }
}