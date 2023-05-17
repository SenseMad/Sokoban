using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using Sokoban.LevelManagement;

namespace Sokoban.UI
{
  public class PauseManager : MonoBehaviour
  {
    [SerializeField, Tooltip("ѕанель паузы")]
    private Panel _pausePanel;

    //--------------------------------------

    private InputHandler inputHandler;

    private LevelManager levelManager;

    private PanelController panelController;

    //======================================

    /// <summary>
    /// True, если игра остановлена
    /// </summary>
    public bool IsPause { get; set; }

    //======================================

    private void Awake()
    {
      inputHandler = InputHandler.Instance;

      levelManager = LevelManager.Instance;

      panelController = PanelController.Instance;
    }

    private void OnEnable()
    {
      inputHandler.AI_Player.UI.Pause.performed += OnPause;
      //levelManager.IsReloadLevel.AddListener(ReloadLevel);
    }

    private void OnDisable()
    {
      inputHandler.AI_Player.UI.Pause.performed -= OnPause;
      //levelManager.IsReloadLevel.RemoveListener(ReloadLevel);
    }

    //======================================

    /// <summary>
    /// ¬ключить/¬ыключить паузу
    /// </summary>
    private void SetIsPause()
    {
      if (levelManager.LevelCompleted)
        return;

      if (!IsPause)
      {
        IsPause = true;
        panelController.ShowPanel(_pausePanel);
      }
      else if (panelController.GetCurrentActivePanel() == _pausePanel)
      {
        if (panelController.listAllOpenPanels.Count > 1)
        {
          panelController.ClosePanel();
        }
        else
        {
          IsPause = false;
          panelController.CloseAllPanels();
        }
      }

      levelManager.IsPause?.Invoke(IsPause);
    }

    //======================================

    /// <summary>
    /// ѕерезугрука уровн¤
    /// </summary>
    private void ReloadLevel()
    {
      SetIsPause();
      levelManager.ReloadLevel();
    }

    /// <summary>
    ///  нопка продолжить
    /// </summary>
    public void ContinueButton()
    {
      SetIsPause();
      panelController.CloseAllPanels();
    }

    /// <summary>
    ///  нопка перезагрузки
    /// </summary>
    public void RestartButton()
    {
      SetIsPause();
      levelManager.ReloadLevel();
    }

    /// <summary>
    ///  нопка выхода в меню
    /// </summary>
    public void ExitMenuButton()
    {
      levelManager.ExitMenu();
    }

    //======================================

    /// <summary>
    /// ѕауза
    /// </summary>
    public void OnPause(InputAction.CallbackContext context)
    {
      SetIsPause();
    }

    //======================================
  }
}