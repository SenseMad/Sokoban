using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

using Sokoban.LevelManagement;
using Sokoban.GameManagement;

namespace Sokoban.UI
{
  public class UILevelComplete : MonoBehaviour
  {
    [Header("ПАНЕЛЬ")]
    [SerializeField, Tooltip("Панель пройденного уровня")]
    private Panel _levelCompletePanel;

    [Header("ТЕКСТ")]
    [SerializeField, Tooltip("Текст времени прохождения уровня")]
    private TextMeshProUGUI _textLevelCompletedTime;
    [SerializeField, Tooltip("Текст количества ходов")]
    private TextMeshProUGUI _textNumberMoves;

    //--------------------------------------

    private InputHandler inputHandler;

    private LevelManager levelManager;

    private PanelController panelController;

    //======================================

    private void Awake()
    {
      inputHandler = InputHandler.Instance;

      levelManager = LevelManager.Instance;

      panelController = PanelController.Instance;
    }

    private void OnEnable()
    {
      inputHandler.AI_Player.UI.Select.performed += OnSelect;
      inputHandler.AI_Player.UI.Reload.performed += OnReload;
      inputHandler.AI_Player.UI.Pause.performed += OnExitMenu;

      levelManager.IsNextLevel.AddListener(panelController.CloseAllPanels);
      levelManager.IsLevelCompleted.AddListener(UpdateText);
    }

    private void OnDisable()
    {
      inputHandler.AI_Player.UI.Select.performed -= OnSelect;
      inputHandler.AI_Player.UI.Reload.performed -= OnReload;
      inputHandler.AI_Player.UI.Pause.performed -= OnExitMenu;

      levelManager.IsNextLevel.RemoveListener(panelController.CloseAllPanels);
      levelManager.IsLevelCompleted.RemoveListener(UpdateText);
    }

    //======================================

    /// <summary>
    /// Обновить текст при завершении уровня
    /// </summary>
    private void UpdateText()
    {
      UpdateTextTime();
      UpdateTextNumberMoves();

      panelController.ShowPanel(_levelCompletePanel);
    }

    /// <summary>
    /// Обновить текст времени проведенного на уровне
    /// </summary>
    private void UpdateTextTime()
    {
      int time = (int)(levelManager.TimeOnLevel * 1000f);
      float formatedTime = (float)time;

      string min = $"{(int)formatedTime / 1000 / 60}";
      string sec = ((int)formatedTime / 1000f % 60f).ToString("f3");

      _textLevelCompletedTime.text = $"ВРЕМЯ ПРОХОЖДЕНИЯ УРОВНЯ: {min}:{sec}";
    }

    /// <summary>
    /// Обновить текст количества ходов
    /// </summary>
    private void UpdateTextNumberMoves()
    {
      _textNumberMoves.text = $"КОЛИЧЕСТВО ХОДОВ: {levelManager.NumberMoves}";
    }

    //======================================

    /// <summary>
    /// Следующий уровень
    /// </summary>
    public void OnSelect(InputAction.CallbackContext context)
    {
      if (!levelManager.LevelCompleted)
        return;

      levelManager.UploadNewLevel();
    }

    /// <summary>
    /// Перезугрузка уровня
    /// </summary>
    public void OnReload(InputAction.CallbackContext context)
    {
      if (!levelManager.LevelCompleted)
        return;

      panelController.CloseAllPanels();
      levelManager.ReloadLevel();
    }

    /// <summary>
    /// Выход в меню
    /// </summary>
    public void OnExitMenu(InputAction.CallbackContext context)
    {
      if (!levelManager.LevelCompleted)
        return;

      levelManager.ExitMenu();
    }

    //======================================
  }
}