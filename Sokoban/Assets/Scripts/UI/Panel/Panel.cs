using UnityEngine;

namespace Sokoban.UI
{
  public class Panel : MonoBehaviour
  {
    [SerializeField, Tooltip("Объект панели")]
    private GameObject _panelObject;

    [SerializeField, Tooltip("True, если панель показана")]
    private bool _panelIsShow;

    //======================================

    private void Awake()
    {
      if (_panelObject == null)
        _panelObject = gameObject;
    }

    //======================================

    /// <summary>
    /// True, если панель показана
    /// </summary>
    public bool PanelIsShow
    {
      get => _panelIsShow;
      set
      {
        _panelIsShow = value;
        _panelObject.SetActive(value);
      }
    }

    //======================================

    /// <summary>
    /// Показать панель
    /// </summary>
    public void ShowPanel()
    {
      PanelIsShow = true;
    }

    /// <summary>
    /// Скрыть панель
    /// </summary>
    public void HidePanel()
    {
      PanelIsShow = false;
    }

    //======================================
  }
}