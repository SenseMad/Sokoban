using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.UI
{
  public class PanelController : MonoBehaviour
  {
    private static PanelController _instance;

    //======================================

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

    //--------------------------------------

    public static PanelController Instance
    {
      get
      {
        if (_instance == null) { _instance = FindObjectOfType<PanelController>(); }
        return _instance;
      }
    }

    //======================================

    private void Awake()
    {
      if (_instance != null && _instance != this)
      {
        Destroy(this);
        return;
      }
      _instance = this;
    }

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
    /// ������ �������� ������
    /// </summary>
    public void HideActivePanel()
    {
      HidePanel(_currentActivePanel);

      listAllOpenPanels.Remove(_currentActivePanel);
      _currentActivePanel = listAllOpenPanels.Count > 1 ? listAllOpenPanels[listAllOpenPanels.Count - 1] : null;
    }

    /// <summary>
    /// ������ ������ � ������� ����� ������
    /// </summary>
    public void SetActivePanel(Panel parPanel)
    {
      HideActivePanel();

      ShowPanel(parPanel);
    }

    //======================================

    /// <summary>
    /// �������� ������� �� �������
    /// </summary>
    public void ClosePanel()
    {
      if (listAllOpenPanels.Count > 1)
      {
        listAllOpenPanels.Remove(_currentActivePanel);
        SetActivePanel(listAllOpenPanels[listAllOpenPanels.Count - 1]);
        return;
      }

      HideActivePanel();
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