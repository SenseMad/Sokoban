using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sokoban.LevelManagement;
using Sokoban.GameManagement;

namespace Sokoban.UI
{
  /// <summary>
  /// ���������������� ��������� ���� ������ �������
  /// </summary>
  public class UILocationSelectMenu : MonoBehaviour
  {
    [SerializeField, Tooltip("������, ���� ����� ����������� ������")]
    private RectTransform _locationSelectPanel;

    [Header("������")]
    [SerializeField, Tooltip("������ ������ ������ �������")]
    private UILocationSelectButton _prefabButtonLocationSelect;

    [Header("������")]
    [SerializeField, Tooltip("������ �������� ������ �������")]
    private Panel _panelOpeningLevelSelection;

    //--------------------------------------

    private GameManager gameManager;

    /// <summary>
    /// ���������� �������
    /// </summary>
    private PanelController panelController;

    /// <summary>
    /// ���� ������ �������
    /// </summary>
    private UILevelSelectMenu uILevelSelectMenu;

    /// <summary>
    /// ������ ������ ������ �������
    /// </summary>
    private List<UILocationSelectButton> listUILocationSelectButton = new List<UILocationSelectButton>();

    //======================================

    private void Awake()
    {
      gameManager = GameManager.Instance;

      panelController = PanelController.Instance;

      uILevelSelectMenu = UILevelSelectMenu.Instance;
    }

    private void Start()
    {
      DisplayLocationSelectionButtonsUI();
    }

    //======================================

    /// <summary>
    /// ���������� ������ ������ ������� � ����������
    /// </summary>
    private void DisplayLocationSelectionButtonsUI()
    {
      foreach (var location in Levels.GetListLocation())
      {
        UILocationSelectButton button = Instantiate(_prefabButtonLocationSelect, _locationSelectPanel);

        button.Button = button.GetComponent<Button>();

        if (gameManager.ProgressData.IsLocationOpen(location))
        {
          button.Button.interactable = true;
          button.Button.onClick.AddListener(() => SelectLocation(location));
        }

        listUILocationSelectButton.Add(button);
        button.Initialize(location);
      }
    }

    //======================================

    /// <summary>
    /// ������� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    private void SelectLocation(Location parLocation)
    {
      panelController.SetActivePanel(_panelOpeningLevelSelection);

      uILevelSelectMenu.DisplayLevelSelectionButtonsUI(parLocation);
    }

    //======================================
  }
}