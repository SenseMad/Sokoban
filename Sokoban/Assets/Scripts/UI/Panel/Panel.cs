using UnityEngine;

namespace Sokoban.UI
{
  public class Panel : MonoBehaviour
  {
    [SerializeField, Tooltip("������ ������")]
    private GameObject _panelObject;

    [SerializeField, Tooltip("True, ���� ������ ��������")]
    private bool _panelIsShow;

    //======================================

    private void Awake()
    {
      if (_panelObject == null)
        _panelObject = gameObject;
    }

    //======================================

    /// <summary>
    /// True, ���� ������ ��������
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
    /// �������� ������
    /// </summary>
    public void ShowPanel()
    {
      PanelIsShow = true;
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    public void HidePanel()
    {
      PanelIsShow = false;
    }

    //======================================
  }
}