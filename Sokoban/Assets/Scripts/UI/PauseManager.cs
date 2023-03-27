using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using LevelManagement;

namespace Sokoban.UI
{
  public class PauseManager : MonoBehaviour
  {
    private static PauseManager _instance;

    //======================================

    [SerializeField, Tooltip("Панель паузы")]
    private Panel _pausePanel;

    //--------------------------------------

    private LevelManager levelManager;

    private PanelController panelController;

    //======================================

    /// <summary>
    /// True, если игра остановлена
    /// </summary>
    public bool IsPause { get; set; }

    //======================================

    public static PauseManager Instance
    {
      get
      {
        if (_instance == null) { _instance = FindObjectOfType<PauseManager>(); }
        return _instance;
      }
    }

    //======================================

    private void Awake()
    {
      levelManager = LevelManager.Instance;

      panelController = PanelController.Instance;

      if (_instance != null && _instance != this)
      {
        Destroy(this);
        return;
      }
      _instance = this;
    }

    private void OnEnable()
    {
      //levelManager.IsReloadLevel.AddListener(ReloadLevel);
    }

    private void OnDisable()
    {
      //levelManager.IsReloadLevel.RemoveListener(ReloadLevel);
    }

    //======================================

    /// <summary>
    /// Включить/Выключить паузу
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
    /// Перезугрука уровня
    /// </summary>
    private void ReloadLevel()
    {
      SetIsPause();
      levelManager.ReloadLevel();
    }

    /// <summary>
    /// Кнопка продолжить
    /// </summary>
    public void ContinueButton()
    {
      SetIsPause();
      panelController.CloseAllPanels();
    }

    /// <summary>
    /// Кнопка перезагрузки
    /// </summary>
    public void RestartButton()
    {
      SetIsPause();
      levelManager.ReloadLevel();
    }

    /// <summary>
    /// Кнопка выхода в меню
    /// </summary>
    public void ExitMenuButton()
    {
      SceneManager.LoadScene($"MenuScene");
    }

    //======================================

    /// <summary>
    /// Пауза
    /// </summary>
    public void OnPause(InputAction.CallbackContext context)
    {
      switch (context.phase)
      {
        case InputActionPhase.Performed:
          SetIsPause();
          break;
      }
    }

    //======================================
  }
}