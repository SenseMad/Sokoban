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
  public class UILocationSelectMenu : MenuUI
  {
    [SerializeField, Tooltip("������, ���� ����� ����������� ������")]
    private RectTransform _locationSelectPanel;

    [Header("������")]
    [SerializeField, Tooltip("������ ������ ������ �������")]
    private UILocationSelectButton _prefabButtonLocationSelect;

    [Header("������")]
    [SerializeField, Tooltip("������ ������ �������")]
    private Panel _panelLevelSelection;

    //--------------------------------------

    private GameManager gameManager;

    /// <summary>
    /// ���� ������ �������
    /// </summary>
    private UILevelSelectMenu uILevelSelectMenu;

    /// <summary>
    /// ������ ������ ������ �������
    /// </summary>
    private List<UILocationSelectButton> listUILocationSelectButton = new List<UILocationSelectButton>();

    //======================================

    protected override void Awake()
    {
      base.Awake();

      gameManager = GameManager.Instance;

      uILevelSelectMenu = _panelLevelSelection.GetComponent<UILevelSelectMenu>();
    }

    protected override void OnEnable()
    {
      DisplayLocationSelectionButtonsUI();

      base.OnEnable();
    }

    protected override void OnDisable()
    {
      base.OnDisable();

      ClearButtonsUI();
    }

    protected override void Update()
    {
      MoveMenuHorizontally();
    }

    //======================================

    protected override void MoveMenuHorizontally()
    {
      if (_listButtons.Count == 0)
        return;

      if (Time.time > nextTimeMoveNextValue)
      {
        nextTimeMoveNextValue = Time.time + timeMoveNextValue;

        if (inputHandler.GetChangingValuesInput() > 0)
        {
          IsSelected = false;

          indexActiveButton++;

          if (indexActiveButton > _listButtons.Count - 1) indexActiveButton = 0;
          if (!gameManager.ProgressData.IsLocationOpen(listUILocationSelectButton[indexActiveButton].Location))
            indexActiveButton = 0;

          Sound();
          IsSelected = true;
        }

        if (inputHandler.GetChangingValuesInput() < 0)
        {
          IsSelected = false;

          indexActiveButton--;

          if (indexActiveButton < 0) indexActiveButton = _listButtons.Count - 1;
          while (!gameManager.ProgressData.IsLocationOpen(listUILocationSelectButton[indexActiveButton].Location))
            indexActiveButton--;

          Sound();
          IsSelected = true;
        }
      }

      if (inputHandler.GetChangingValuesInput() == 0)
      {
        nextTimeMoveNextValue = Time.time;
      }
    }

    //======================================

    /// <summary>
    /// ���������� ������ ������ ������� � ����������
    /// </summary>
    private void DisplayLocationSelectionButtonsUI()
    {
      if (_listButtons.Count != 0)
        return;

      foreach (var location in Levels.GetListLocation())
      {
        UILocationSelectButton button = Instantiate(_prefabButtonLocationSelect, _locationSelectPanel);

        button.Button = button.GetComponent<Button>();

        if (gameManager.ProgressData.IsLocationOpen(location))
        {
          button.ChangeColor();
          button.Button.interactable = true;
          button.Button.onClick.AddListener(() => SelectLocation(location));
        }

        listUILocationSelectButton.Add(button);
        _listButtons.Add(button.GetComponent<Button>());
        button.Initialize(location);
      }
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    public void ClearButtonsUI()
    {
      for (int i = 0; i < listUILocationSelectButton.Count; i++)
      {
        Destroy(listUILocationSelectButton[i].gameObject);
      }

      listUILocationSelectButton = new List<UILocationSelectButton>();
      _listButtons = new List<Button>();
    }

    //======================================

    /// <summary>
    /// ������� �������
    /// </summary>
    /// <param name="parLocation">�������</param>
    private void SelectLocation(Location parLocation)
    {
      panelController.SetActivePanel(_panelLevelSelection);

      uILevelSelectMenu.DisplayLevelSelectionButtonsUI(parLocation);
    }

    //======================================
  }
}