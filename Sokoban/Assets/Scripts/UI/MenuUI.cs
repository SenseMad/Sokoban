using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Sokoban.UI
{
  public abstract class MenuUI : MonoBehaviour
  {
    [SerializeField, Tooltip("True, ���� ������ ������� ����")]
    private bool _menuCannotClosed = false;

    [SerializeField, Tooltip("������ ������")]
    protected List<Button> _listButtons;

    //--------------------------------------

    private InputHandler inputHandler;

    private PanelController panelController;

    /// <summary>
    /// True, ���� ������ �������
    /// </summary>
    private bool isSelected;

    /// <summary>
    /// ������ �������� ������
    /// </summary>
    protected int indexActiveButton;

    /// <summary>
    /// ����� �������� � ���������� ��������
    /// </summary>
    private readonly float timeMoveNextValue = 0.2f;
    private float nextTimeMoveNextValue = 0.0f;

    //======================================

    /// <summary>
    /// True, ���� ������ �������
    /// </summary>
    public bool IsSelected
    {
      get => isSelected;
      set
      {
        isSelected = value;
        if (isSelected)
          OnSelected();
        else
          OnDeselected();
      }
    }

    //======================================

    protected virtual void Awake()
    {
      inputHandler = InputHandler.Instance;

      panelController = PanelController.Instance;
    }

    private void OnEnable()
    {
      inputHandler.AI_Player.UI.Select.performed += Select_performed;
      inputHandler.AI_Player.UI.Pause.performed += OnCloseMenu;

      indexActiveButton = 0;
      IsSelected = true;
    }

    private void OnDisable()
    {
      inputHandler.AI_Player.UI.Select.performed -= Select_performed;
      inputHandler.AI_Player.UI.Pause.performed -= OnCloseMenu;

      IsSelected = false;
    }

    private void Update()
    {
      ChangeActiveButton();
    }

    //======================================

    #region ������� ����

    /// <summary>
    /// ������� ����
    /// </summary>
    public void CloseMenu()
    {
      if (_menuCannotClosed)
        return;

      Sound();
      panelController.ClosePanel();
    }

    private void OnCloseMenu(InputAction.CallbackContext context)
    {
      CloseMenu();
    }

    #endregion

    #region ����� ������

    private void Select_performed(InputAction.CallbackContext obj)
    {
      _listButtons[indexActiveButton].onClick?.Invoke();
    }

    #endregion

    #region �����

    private void Sound()
    {

    }

    #endregion

    /// <summary>
    /// �������� �������� ������
    /// </summary>
    private void ChangeActiveButton()
    {
      if (Time.time > nextTimeMoveNextValue)
      {
        nextTimeMoveNextValue = Time.time + timeMoveNextValue;

        if (inputHandler.GetNavigationInput() > 0)
        {
          IsSelected = false;

          indexActiveButton--;

          if (indexActiveButton < 0) indexActiveButton = _listButtons.Count - 1;

          Sound();
          IsSelected = true;
        }

        if (inputHandler.GetNavigationInput() < 0)
        {
          IsSelected = false;

          indexActiveButton++;

          if (indexActiveButton > _listButtons.Count - 1) indexActiveButton = 0;

          Sound();
          IsSelected = true;
        }
      }

      if (inputHandler.GetNavigationInput() == 0)
      {
        nextTimeMoveNextValue = Time.time;
      }
    }

    //======================================

    protected virtual void OnSelected()
    {
      var button = _listButtons[indexActiveButton].GetComponentInChildren<TextMeshProUGUI>();
      if (button != null)
        button.color = ColorsGame.SELECTED_COLOR;
    }

    protected virtual void OnDeselected()
    {
      var button = _listButtons[indexActiveButton].GetComponentInChildren<TextMeshProUGUI>();
      if (button != null)
        button.color = ColorsGame.STANDART_COLOR;
    }

    //======================================
  }
}