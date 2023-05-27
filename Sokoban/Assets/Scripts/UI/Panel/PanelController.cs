using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.UI
{
  public class PanelController : SingletonInSceneNoInstance<PanelController>
  {
    [SerializeField, Tooltip("Текущая активная панель")]
    private Panel _currentActivePanel;

    //--------------------------------------

    /// <summary>
    /// Список всех открытых панелей
    /// </summary>
    public List<Panel> listAllOpenPanels = new List<Panel>();

    //======================================

    /// <summary>
    /// Получить текущую активную панель
    /// </summary>
    public Panel GetCurrentActivePanel() => _currentActivePanel;

    //======================================

    /// <summary>
    /// Показать панель
    /// </summary>
    /// <param name="parPanel">Панель которую нужно показать</param>
    public void ShowPanel(Panel parPanel)
    {
      _currentActivePanel = parPanel;

      if (_currentActivePanel == null)
        return;

      _currentActivePanel.ShowPanel();
      listAllOpenPanels.Add(parPanel);
    }

    /// <summary>
    /// Скрыть панель
    /// </summary>
    /// <param name="parPanel">Панель которую нужно скрыть</param>
    public void HidePanel(Panel parPanel)
    {
      if (parPanel == null)
        return;

      parPanel.HidePanel();
    }

    /// <summary>
    /// Скрыть старую и открыть новую панель
    /// </summary>
    public void SetActivePanel(Panel parPanel)
    {
      HidePanel(_currentActivePanel);

      ShowPanel(parPanel);
    }

    //======================================

    /// <summary>
    /// Закрытие панелей по порядку
    /// </summary>
    public void ClosePanel()
    {
      if (listAllOpenPanels.Count == 0 || _currentActivePanel == null)
        return;

      HidePanel(_currentActivePanel);
      listAllOpenPanels.Remove(_currentActivePanel);

      if (listAllOpenPanels.Count == 0)
      {
        _currentActivePanel = null;
        return;
      }

      _currentActivePanel = listAllOpenPanels[listAllOpenPanels.Count - 1];
      _currentActivePanel.ShowPanel();
    }

    /// <summary>
    /// Закрыть все панели
    /// </summary>
    public void CloseAllPanels()
    {
      for (int i = 0; i < listAllOpenPanels.Count; i++)
      {
        ClosePanel();
      }
    }

    /// <summary>
    /// Закрыть все панели
    /// </summary>
    public void CloseAllPanels1()
    {
      for (int i = 0; i < listAllOpenPanels.Count; i++)
      {
        HidePanel(listAllOpenPanels[i]);
        listAllOpenPanels.Remove(listAllOpenPanels[i]);
      }

      _currentActivePanel = null;
      listAllOpenPanels.Clear();
    }

    //======================================
  }
}