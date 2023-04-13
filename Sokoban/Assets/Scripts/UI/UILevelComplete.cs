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
    [Header("������")]
    [SerializeField, Tooltip("������ ����������� ������")]
    private Panel _levelCompletePanel;

    [Header("�����")]
    [SerializeField, Tooltip("����� ������� ����������� ������")]
    private TextMeshProUGUI _textLevelCompletedTime;
    [SerializeField, Tooltip("����� ���������� �����")]
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
    /// �������� ����� ��� ���������� ������
    /// </summary>
    private void UpdateText()
    {
      UpdateTextTime();
      UpdateTextNumberMoves();

      panelController.ShowPanel(_levelCompletePanel);
    }

    /// <summary>
    /// �������� ����� ������� ������������ �� ������
    /// </summary>
    private void UpdateTextTime()
    {
      int time = (int)(levelManager.TimeOnLevel * 1000f);
      float formatedTime = (float)time;

      string min = $"{(int)formatedTime / 1000 / 60}";
      string sec = ((int)formatedTime / 1000f % 60f).ToString("f3");

      _textLevelCompletedTime.text = $"����� ����������� ������: {min}:{sec}";
    }

    /// <summary>
    /// �������� ����� ���������� �����
    /// </summary>
    private void UpdateTextNumberMoves()
    {
      _textNumberMoves.text = $"���������� �����: {levelManager.NumberMoves}";
    }

    //======================================

    /// <summary>
    /// ��������� �������
    /// </summary>
    public void OnSelect(InputAction.CallbackContext context)
    {
      if (!levelManager.LevelCompleted)
        return;

      levelManager.UploadNewLevel();
    }

    /// <summary>
    /// ������������ ������
    /// </summary>
    public void OnReload(InputAction.CallbackContext context)
    {
      if (!levelManager.LevelCompleted)
        return;

      panelController.CloseAllPanels();
      levelManager.ReloadLevel();
    }

    /// <summary>
    /// ����� � ����
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