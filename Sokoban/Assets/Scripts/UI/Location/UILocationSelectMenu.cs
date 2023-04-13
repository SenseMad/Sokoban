using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sokoban.LevelManagement;
using Sokoban.GameManagement;

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

    [Header("ПАНЕЛЬ")]
    [SerializeField, Tooltip("Панель открытия выбора уровней")]
    private Panel _panelOpeningLevelSelection;

    //--------------------------------------

    private GameManager gameManager;

    /// <summary>
    /// Контроллер панелей
    /// </summary>
    private PanelController panelController;

    /// <summary>
    /// Меню выбора уровней
    /// </summary>
    private UILevelSelectMenu uILevelSelectMenu;

    /// <summary>
    /// Список кнопок выбора локаций
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
    /// Отобразить кнопки выбора локации в интерфейсе
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
    /// Выбрать локацию
    /// </summary>
    /// <param name="parLocation">Локация</param>
    private void SelectLocation(Location parLocation)
    {
      panelController.SetActivePanel(_panelOpeningLevelSelection);

      uILevelSelectMenu.DisplayLevelSelectionButtonsUI(parLocation);
    }

    //======================================
  }
}