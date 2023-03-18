using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using LevelManagement;
using GameManagement;

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

    [Header("���� ������ �������")]
    [SerializeField, Tooltip("���� ������ ������ ����������������� ����������")]
    private UILevelSelectMenu _uILevelSelectMenu;

    [Header("������")]
    [SerializeField, Tooltip("PanelController")]
    private PanelController _panelController;
    [SerializeField, Tooltip("������ �������� ������ �������")]
    private Panel _panelOpeningLevelSelection;

    //--------------------------------------

    private GameManager gameManager;

    /// <summary>
    /// ������ ������ ������ �������
    /// </summary>
    private List<UILocationSelectButton> listUILocationSelectButton = new List<UILocationSelectButton>();

    //======================================

    private void Start()
    {
      gameManager = GameManager.Instance;

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
        //Debug.Log(gameManager.ProgressData.GetNumberLevelsCompleted(Location.Winter));
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
      _panelController.SetActivePanel(_panelOpeningLevelSelection);

      _uILevelSelectMenu.ClearButtonsUI();
      _uILevelSelectMenu.DisplayLevelSelectionButtonsUI(parLocation);
    }

    //======================================
  }
}