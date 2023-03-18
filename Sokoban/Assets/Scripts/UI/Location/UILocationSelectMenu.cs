using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using LevelManagement;
using GameManagement;

namespace Sokoban.UI
{
  /// <summary>
  /// Пользовательский интерфейс меню выбора локации
  /// </summary>
  public class UILocationSelectMenu : MonoBehaviour
  {
    [SerializeField, Tooltip("Панель, куда будут создаваться кнопки")]
    private RectTransform _locationSelectPanel;

    [Header("ПРЕФАБ")]
    [SerializeField, Tooltip("Префаб кнопки выбора локации")]
    private UILocationSelectButton _prefabButtonLocationSelect;

    [Header("МЕНЮ ВЫБОРА УРОВНЕЙ")]
    [SerializeField, Tooltip("Меню выбора уровня пользовательского интерфейса")]
    private UILevelSelectMenu _uILevelSelectMenu;

    [Header("ПАНЕЛЬ")]
    [SerializeField, Tooltip("PanelController")]
    private PanelController _panelController;
    [SerializeField, Tooltip("Панель открытия выбора уровней")]
    private Panel _panelOpeningLevelSelection;

    //--------------------------------------

    private GameManager gameManager;

    /// <summary>
    /// Список кнопок выбора локаций
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
    /// Отобразить кнопки выбора локации в интерфейсе
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
    /// Выбрать локацию
    /// </summary>
    /// <param name="parLocation">Локация</param>
    private void SelectLocation(Location parLocation)
    {
      _panelController.SetActivePanel(_panelOpeningLevelSelection);

      _uILevelSelectMenu.ClearButtonsUI();
      _uILevelSelectMenu.DisplayLevelSelectionButtonsUI(parLocation);
    }

    //======================================
  }
}