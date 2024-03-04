using UnityEngine;
using UnityEngine.InputSystem;

using Sokoban.LevelManagement;

namespace Sokoban.UI
{
  public class PauseManager : MenuUI
  {
    [SerializeField, Tooltip("Панель паузы")]
    private Panel _pausePanel;

    //--------------------------------------

    private bool isPause;

    private LevelManager levelManager;

    //======================================

    private bool IsPause
    {
      get => isPause;
      set
      {
        isPause = value;
        levelManager.IsPause = isPause;
        //levelManager.SetPause(value);
      }
    }

    //======================================

    protected override void Awake()
    {
      base.Awake();

      levelManager = LevelManager.Instance;
    }

    protected override void OnEnable()
    {
      indexActiveButton = 0;

      base.OnEnable();

      inputHandler.AI_Player.UI.Pause.performed += OnPause;

      levelManager.OnPauseEvent.AddListener(OnPause);
    }

    protected override void OnDisable()
    {
      base.OnDisable();

      inputHandler.AI_Player.UI.Pause.performed -= OnPause;

      levelManager.OnPauseEvent.RemoveListener(OnPause);
    }

    //======================================

    /// <summary>
    /// Включить/Выключить паузу
    /// </summary>
    private void SetIsPause()
    {
      /*if (levelManager.LevelCompleted)
        return;

      if (!IsPause)
      {
        IsPause = true;
        panelController.ShowPanel(_pausePanel);
      }
      else
      {

      }

      Debug.Log(IsPause);*/
    }

    private void OnPause(bool parValue)
    {
      IsPause = false;
    }

    //======================================

    protected override void CloseMenu()
    {
      if (!levelManager.GridLevel.IsLevelCreated)
        return;

      if (!IsPause)
      {
        if (levelManager.LevelCompleted)
          return;

        IsPause = true;
        panelController.ShowPanel(_pausePanel);
        return;
      }
      
      if (panelController.GetCurrentActivePanel() == _pausePanel)
        base.CloseMenu();

      if (panelController.listAllOpenPanels.Count != 0)
      {
        if (panelController.GetCurrentActivePanel() != _pausePanel)
          return;
      }

      IsPause = false;

      IsSelected = false;
      indexActiveButton = 0;
      IsSelected = true;
    }

    protected override void ButtonClick()
    {
      if (panelController.GetCurrentActivePanel() != _pausePanel)
        return;

      base.ButtonClick();
    }

    protected override void MoveMenuVertically(int parValue)
    {
      if (panelController.GetCurrentActivePanel() != _pausePanel)
        return;

      base.MoveMenuVertically(parValue);
    }

    //======================================

    public void ContinueButton()
    {
      IsSelected = false;
      indexActiveButton = 0;
      IsSelected = true;

      IsPause = false;
      panelController.CloseAllPanels();
    }

    public void RestartButton()
    {
      ContinueButton();
      levelManager.ReloadLevel();
    }

    public void ExitMenuButton()
    {
      IsPause = false;
      levelManager.ExitMenu();
    }

    //======================================       

    public void OnPause(InputAction.CallbackContext context)
    {
      SetIsPause();
    }

    //======================================
  }
}