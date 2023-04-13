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
    [SerializeField, Tooltip("������ �����")]
    private Panel _pausePanel;

    //--------------------------------------

    private InputHandler inputHandler;

    private LevelManager levelManager;

    private PanelController panelController;

    //======================================

    /// <summary>
    /// True, ���� ���� �����������
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
    /// ��������/��������� �����
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
    /// ����������� ������
    /// </summary>
    private void ReloadLevel()
    {
      SetIsPause();
      levelManager.ReloadLevel();
    }

    /// <summary>
    /// ������ ����������
    /// </summary>
    public void ContinueButton()
    {
      SetIsPause();
      panelController.CloseAllPanels();
    }

    /// <summary>
    /// ������ ������������
    /// </summary>
    public void RestartButton()
    {
      SetIsPause();
      levelManager.ReloadLevel();
    }

    /// <summary>
    /// ������ ������ � ����
    /// </summary>
    public void ExitMenuButton()
    {
      levelManager.ExitMenu();
    }

    //======================================

    /// <summary>
    /// �����
    /// </summary>
    public void OnPause(InputAction.CallbackContext context)
    {
      SetIsPause();
    }

    //======================================
  }
}