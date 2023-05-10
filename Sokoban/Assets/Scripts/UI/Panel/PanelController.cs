using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.UI
{
  public class PanelController : SingletonInSceneNoInstance<PanelController>
  {
    [SerializeField, Tooltip("������� �������� ������")]
    private Panel _currentActivePanel;

    //--------------------------------------

    /// <summary>
    /// ������ ���� �������� �������
    /// </summary>
    public List<Panel> listAllOpenPanels = new List<Panel>();

    //======================================

    /// <summary>
    /// �������� ������� �������� ������
    /// </summary>
    public Panel GetCurrentActivePanel() => _currentActivePanel;

    //======================================

    /// <summary>
    /// �������� ������
    /// </summary>
    /// <param name="parPanel">������ ������� ����� ��������</param>
    public void ShowPanel(Panel parPanel)
    {
      _currentActivePanel = parPanel;

      if (_currentActivePanel == null)
        return;

      _currentActivePanel.ShowPanel();
      listAllOpenPanels.Add(parPanel);
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    /// <param name="parPanel">������ ������� ����� ������</param>
    public void HidePanel(Panel parPanel)
    {
      if (parPanel == null)
        return;

      parPanel.HidePanel();
    }

    /// <summary>
    /// ������ ������ � ������� ����� ������
    /// </summary>
    public void SetActivePanel(Panel parPanel)
    {
      HidePanel(_currentActivePanel);

      ShowPanel(parPanel);
    }

    //======================================

    /// <summary>
    /// �������� ������� �� �������
    /// </summary>
    public void ClosePanel()
    {
      HidePanel(_currentActivePanel);

      listAllOpenPanels.Remove(_currentActivePanel);

      _currentActivePanel = listAllOpenPanels[listAllOpenPanels.Count - 1];

      if (_currentActivePanel == null)
        return;

      _currentActivePanel.ShowPanel();
    }

    /// <summary>
    /// ������� ��� ������
    /// </summary>
    public void CloseAllPanels()
    {
      for (int i = 0; i < listAllOpenPanels.Count; i++)
      {
        ClosePanel();
      }
    }

    //======================================
  }
}