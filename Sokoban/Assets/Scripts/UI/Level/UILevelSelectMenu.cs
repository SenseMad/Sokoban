using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using LevelManagement;
using GameManagement;

namespace Sokoban.UI
{
  /// <summary>
  /// Пользовательский интерфейс меню выбора уровня
  /// </summary>
  public class UILevelSelectMenu : MonoBehaviour
  {
    [SerializeField, Tooltip("Панель, куда будут создаваться кнопки")]
    private RectTransform _levelSelectPanel;

    [Header("ПРЕФАБ")]
    [SerializeField, Tooltip("Префаб кнопки выбора уровня")]
    private UILevelSelectButton _prefabButtonLevelSelect;

    //--------------------------------------

    private GameManager gameManager;

    /// <summary>
    /// Список кнопок выбора уровней
    /// </summary>
    private List<UILevelSelectButton> listUILevelSelectButton = new List<UILevelSelectButton>();

    //======================================

    private void Awake()
    {
      gameManager = GameManager.Instance;
    }

    //======================================

    /// <summary>
    /// Отобразить кнопки выбора уровня в интерфейсе
    /// </summary>
    public void DisplayLevelSelectionButtonsUI(Location parLocation)
    {
      foreach (var levelData in Levels.GetListLevelData(parLocation))
      {
        UILevelSelectButton button = Instantiate(_prefabButtonLevelSelect, _levelSelectPanel);

        button.Button = button.GetComponent<Button>();
        if (gameManager.ProgressData.GetNumberLevelsCompleted(parLocation) >= levelData.LevelNumber - 1)
        {
          button.Button.interactable = true;
          button.Button.onClick.AddListener(() => SelectLevel(levelData));
        }

        listUILevelSelectButton.Add(button);
        button.Initialize(levelData);
      }
    }

    /// <summary>
    /// Очистить список кнопок
    /// </summary>
    public void ClearButtonsUI()
    {
      if (listUILevelSelectButton.Count < 1)
        return;

      foreach (var levelSelectButton in listUILevelSelectButton)
      {
        Destroy(levelSelectButton.gameObject);
      }

      listUILevelSelectButton = new List<UILevelSelectButton>();
    }

    //======================================

    /// <summary>
    /// Выбрать уровень
    /// </summary>
    private void SelectLevel(LevelData levelData)
    {
      Levels.CurrentSelectedLevelData = levelData;

      SceneManager.LoadScene($"GameScene");
    }

    //======================================
  }
}