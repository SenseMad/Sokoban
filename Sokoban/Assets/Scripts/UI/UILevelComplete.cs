using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using LevelManagement;
using GameManagement;
using UnityEngine.InputSystem;

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

    private LevelManager levelManager;

    private PanelController panelController;

    //======================================

    private void Awake()
    {
      levelManager = LevelManager.Instance;

      panelController = PanelController.Instance;
    }

    private void OnEnable()
    {
      levelManager.IsNextLevel.AddListener(CloseMenu);
      levelManager.IsLevelCompleted.AddListener(UpdateText);
    }

    private void OnDisable()
    {
      levelManager.IsNextLevel.RemoveListener(CloseMenu);
      levelManager.IsLevelCompleted.RemoveListener(UpdateText);
    }

    //======================================

    /// <summary>
    /// �������� ����� ��� ���������� ������
    /// </summary>
    private void UpdateText()
    {
      UpdateTextTime();
      UodateTextNumberMoves();

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
    private void UodateTextNumberMoves()
    {
      _textNumberMoves.text = $"���������� �����: {levelManager.NumberMoves}";
    }

    //======================================

    /// <summary>
    /// ������ ������������
    /// </summary>
    public void RestartButton()
    {
      panelController.CloseAllPanels();
      levelManager.ReloadLevel();
    }

    /// <summary>
    /// ������� ����
    /// </summary>
    private void CloseMenu()
    {
      panelController.CloseAllPanels();
    }

    //======================================

    /// <summary>
    /// ������������ ������
    /// </summary>
    public void OnReload(InputAction.CallbackContext context)
    {
      switch (context.phase)
      {
        case InputActionPhase.Performed:
          panelController.CloseAllPanels();
          levelManager.ReloadLevel();
          break;
      }
    }

    //======================================
  }
}