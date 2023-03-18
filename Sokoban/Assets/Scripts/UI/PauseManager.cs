using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LevelManagement;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Sokoban.UI
{
  public class PauseManager : MonoBehaviour
  {
    [SerializeField, Tooltip("������ �����")]
    private Panel _pausePanel;

    [SerializeField, Tooltip("PanelController")]
    private PanelController _panelController;

    //--------------------------------------

    private LevelManager levelManager;

    /// <summary>
    /// True, ���� ���� �����������
    /// </summary>
    public bool IsPause { get; set; }

    //======================================



    //======================================

    private void Awake()
    {
      levelManager = LevelManager.Instance;
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
        _panelController.ShowPanel(_pausePanel);
      }
      else if (_panelController.GetCurrentActivePanel() == _pausePanel)
      {
        if (_panelController.listAllOpenPanels.Count > 1)
        {
          _panelController.ClosePanel();
        }
        else
        {
          IsPause = false;
          _panelController.CloseAllPanels();
        }
      }

      levelManager.IsPause?.Invoke(IsPause);
    }

    //======================================

    /// <summary>
    /// ������ ����������
    /// </summary>
    public void ContinueButton()
    {
      IsPause = false;
      _panelController.CloseAllPanels();
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
      SceneManager.LoadScene($"MenuScene");
    }

    //======================================

    /// <summary>
    /// �����
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