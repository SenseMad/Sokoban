using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

using Sokoban.LevelManagement;

namespace Sokoban.UI
{
  public class UILevelComplete : MonoBehaviour
  {
    [Header("ПАНЕЛЬ")]
    [SerializeField, Tooltip("Панель пройденного уровн¤")]
    private Panel _levelCompletePanel;

    [Header("ТЕКСТЫ")]
    [SerializeField, Tooltip("Текст времени прохождени¤ уровн¤")]
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
      UpdateTextTimeLevel();
      UpdateTextNumberMoves();

      panelController.ShowPanel(_levelCompletePanel);
    }

    /// <summary>
    /// Обновить текст времени на уровне
    /// </summary>
    private void UpdateTextTimeLevel()
    {
      _textLevelCompletedTime.text = $"Время прохожения: {levelManager.UpdateTextTimeLevel()}";
    }

    /// <summary>
    /// Обновить текст количества ходов
    /// </summary>
    private void UpdateTextNumberMoves()
    {
      _textNumberMoves.text = $"Количество ходов: {levelManager.NumberMoves}";
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
    /// Перезагрузка уровнЯ
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